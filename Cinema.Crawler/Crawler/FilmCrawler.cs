using Cinema.Data.Infrastructure;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Cinema.Crawler.Crawler
{
    public class FilmCrawler : BaseCrawler<Film>
    {
        private const string filmUrl = "http://lichchieu.net/phim";

        private ScheduleRepository _scheduleRepository;

        private FilmRepository FilmRepository { get { return (FilmRepository)_repository; } }

        public FilmCrawler()
            : base()
        {
            _repository = new FilmRepository();
            _scheduleRepository = new ScheduleRepository();
        }

        public new List<Film> CrawlerData()
        {
            return LoadFilms().Result;
        }

        private async Task<List<Film>> LoadFilms()
        {
            HtmlDocument document = _htmlWeb.Load(filmUrl);
            var listFilmHtml = document.DocumentNode.QuerySelectorAll("ul.movie-list > li > div");
            var listTaskFilm = new List<Task<Film>>();
            //var filmHtml = listFilmHtml.First();
            foreach (var filmHtml in listFilmHtml)
            {
                var filmCrawler = new Film();                
                var urlFilm = filmHtml.QuerySelector("h3.movie-title > a").Attributes["href"].Value;
                filmCrawler.ID = int.Parse(urlFilm.Split('/')[2]);
                try
                {
                    if (filmHtml.QuerySelector("div.hot-ribbon").InnerText.ToUpper().Trim() == "HOT")
                    {
                        filmCrawler.IsHot = true;
                    }
                    else
                    {
                        filmCrawler.IsHot = false;
                    }
                }
                catch (Exception)
                {
                    filmCrawler.IsHot = false;
                }
                if (!FilmRepository.Contains(filmCrawler.ID))
                {
                    filmCrawler.Name = HttpUtility.HtmlDecode(filmHtml.QuerySelector("h3.movie-title > a").ChildNodes[0].InnerText.Trim());
                    filmCrawler.LinkPoster = filmHtml.QuerySelector("img.poster").Attributes["src"].Value.Replace("/w80/", "/w800/").Trim();
                    try
                    {
                        //var loadFilmTask = new Task<Film>(() => LoadFilm(filmCrawler, urlFilm).Result);
                        //loadFilmTask.Start();
                        listTaskFilm.Add(LoadFilm(filmCrawler, urlFilm));
                    }
                    catch (Exception e)
                    {
                        new ErrorRepository().Add(new Error()
                        {
                            Action = "Update cinema " + filmCrawler.Name,
                            CreatedDate = DateTime.Now,
                            Message = e.Message,
                            StackTrace = e.StackTrace
                        });
                    }
                }
                else
                {
                    var temp = filmCrawler;
                    filmCrawler = FilmRepository.Get(filmCrawler.ID);
                    filmCrawler.IsHot = temp.IsHot;
                    filmCrawler.LinkPoster = temp.LinkPoster;
                    _scheduleRepository.Delete(s => s.FilmID == filmCrawler.ID);
                    var loadFilmTask = new Task<Film>(() => LoadFilmSchedule(filmCrawler, urlFilm).Result);
                    loadFilmTask.Start();
                    listTaskFilm.Add(loadFilmTask);
                }
            }
            await Task.WhenAll(listTaskFilm);
            var listFilm = new List<Film>();
            foreach (var task in listTaskFilm)
            {
                listFilm.Add(task.Result);
            }
            return listFilm;
        }

        private async Task<Film> LoadFilm(Film film, string url)
        {
            LoadFilmInfo(ref film, url.Replace("/phim/", "/thong-tin-phim/"));
            var newFilm = await LoadFilmSchedule(film, url);
            return newFilm;
        }
        private void LoadFilmInfo(ref Film film, string url)
        {
            HtmlDocument document = _htmlWeb.Load(filmUrl.Replace("/phim", url));
            var htmlInfo = document.DocumentNode.QuerySelectorAll("div.movie-info > table > tr").ToList();
            var stringPremiere = "";
            foreach (var item in htmlInfo)
            {
                switch (item.QuerySelector("th").InnerText.Trim())
                {
                    case "Ngày khởi chiếu":
                        stringPremiere += item.QuerySelector("td").InnerText.Trim();
                        break;
                    case "Thời lượng":
                        film.Time = HttpUtility.HtmlDecode(item.QuerySelector("td").InnerText.Trim());
                        break;
                    case "Thể loại":
                        film.Genre = HttpUtility.HtmlDecode(item.QuerySelector("td").InnerText.Trim());
                        break;
                    case "Phân loại người xem":
                        film.Classification = HttpUtility.HtmlDecode(item.QuerySelector("td").InnerText.Trim());
                        break;
                    case "Diễn viên":
                        film.Actor = HttpUtility.HtmlDecode(item.QuerySelector("td").InnerText.Trim());
                        break;
                    case "Đạo diễn":
                        film.Director = HttpUtility.HtmlDecode(item.ChildNodes[3].InnerText.Trim());
                        break;
                    case "Năm":
                        stringPremiere += "/" + item.ChildNodes[3].InnerText.Trim();
                        break;
                    case "Sản xuất":
                        film.Country = HttpUtility.HtmlDecode(item.ChildNodes[3].InnerText.Trim());
                        break;
                }
            }
            try
            {
                film.Premiere = DateTime.ParseExact(stringPremiere, "dd/MM/yyyy", null);
            }
            catch (Exception) { }
            var htmlTrailer = document.DocumentNode.QuerySelectorAll("script").ToList();
            film.LinkTrailer = subTrailer(htmlTrailer[htmlTrailer.Count - 2].InnerHtml.Trim());
            film.Intro = HttpUtility.HtmlDecode(document.DocumentNode.QuerySelector("div.movie-description").InnerText.Trim());
            new FilmRepository().Add(film);
        }

        private async Task<Film> LoadFilmSchedule(Film film, string url)
        {
            var listTask = new List<Task<List<Schedule>>>();
            HtmlDocument document = _htmlWeb.Load(filmUrl.Replace("/phim", url));
            film.LinkImage = document.DocumentNode.QuerySelector("img.img-responsive").Attributes["src"].Value.Trim().Replace("/w180/", "/w800/");
            try
            {
                film.IMDB = decimal.Parse(document.DocumentNode.QuerySelector("div.imdb > div.number").InnerText.Trim());
            }
            catch
            {
                film.IMDB = 0;
            }
            new FilmRepository().Update(film);
            var listHtmlDate = document.DocumentNode.QuerySelectorAll("div.date-picker > ul > li > a").ToList();
            var firstDate = listHtmlDate[0].Attributes["href"].Value.Trim();
            var firstTask = new Task<List<Schedule>>(() => LoadScheduleFromHtml(film.ID, document, firstDate.Substring(firstDate.Length - 10, 10)));
            firstTask.Start();
            listTask.Add(firstTask);
            for (int i = 1; i < listHtmlDate.Count; i++)
            {
                var index = i;
                var task = new Task<List<Schedule>>(() => LoadSchedule(film.ID, listHtmlDate[index].Attributes["href"].Value.Trim()));
                task.Start();
                listTask.Add(task);
            }
            await Task.WhenAll(listTask);
            var listSchedule = new List<Schedule>();
            foreach(var task in listTask)
            {
                listSchedule.AddRange(task.Result);
            }
            film.Schedules = listSchedule;
            return film;
        }
        private List<Schedule> LoadSchedule(int filmID, string url)
        {
            var document = _htmlWeb.Load(filmUrl.Replace("/phim", url));            
            return LoadScheduleFromHtml(filmID, document, url.Substring(url.Length - 10, 10));
        }
        private List<Schedule> LoadScheduleFromHtml(int filmID, HtmlDocument document, string date)
        {
            var listSchedule = new List<Schedule>();
            Schedule schedule;
            var listHtmlSchedule = document.DocumentNode.QuerySelectorAll("div.cinema-group > div.panel").ToList();
            foreach (var htmlSchedule in listHtmlSchedule)
            {
                var cinemaID = int.Parse(htmlSchedule.QuerySelector("div.panel-sub-title > a.to-cinema").Attributes["href"].Value.Trim().Split('/')[2]);
                var listHtmlValue = htmlSchedule.QuerySelectorAll("div.cinema-schedule > a").ToList();
                foreach (var value in listHtmlValue)
                {
                    string datetime = date + "#" + value.InnerText.Trim().Replace(".", ":");
                    schedule = new Schedule();
                    schedule.FilmID = filmID;
                    schedule.CinemaID = cinemaID;
                    schedule.DateTime = DateTime.ParseExact(datetime, "yyyy-MM-dd#HH:mm", null);
                    schedule.ID = schedule.FilmID + "/" + schedule.CinemaID + "/" + datetime;
                    if (!new ScheduleRepository().Contains(schedule.ID) && listSchedule.FirstOrDefault(s => s.ID == schedule.ID) == null)
                    {
                        schedule.Type = HttpUtility.HtmlDecode(htmlSchedule.QuerySelector("span.slot-group-label").InnerText.Trim().Replace(" ", ""));
                        string strEncoded = value.Attributes["href"].Value.Trim().Replace("/chuyen-tiep?url=", "");
                        schedule.LinkTicket = HttpUtility.UrlDecode(WebUtility.UrlDecode(strEncoded));
                        if (schedule.LinkTicket.Contains("alert("))
                        {
                            schedule.LinkTicket = "null";
                        }
                        listSchedule.Add(schedule);
                    }
                }

            }
            new ScheduleRepository().AddRange(listSchedule);
            return listSchedule;
        }
        private String subTrailer(String text)
        {
            try
            {
                String trailer = text.Substring(text.IndexOf("'//"), text.IndexOf("?origin=") - text.IndexOf("'//"));
                return trailer.Replace("'//", "https://").Trim();
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}