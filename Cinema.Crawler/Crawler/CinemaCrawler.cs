using Cinema.Data.Infrastructure;
using Cinema.Data.Repositories;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cinema.Crawler.Crawler
{
    public class CinemaCrawler : BaseCrawler<Model.Models.Cinema>
    {
        private const string cinemaUrl = "http://lichchieu.net/rap";

        private CinemaChainRepository _cinemaChainRepository;

        private CinemaRepository CinemaRepository { get { return (CinemaRepository)_repository; } }

        public CinemaCrawler()
            :base()
        {
            _repository = new CinemaRepository(new DbFactory());
            _cinemaChainRepository = new CinemaChainRepository(new DbFactory());
        }

        public async Task<List<Model.Models.Cinema>> Crawler()
        {
            HtmlDocument document = _htmlWeb.Load(cinemaUrl);
            var listCinemaHtml = document.DocumentNode.QuerySelectorAll("a.list-group-item");
            var listTaskCinema = new List<Task<Model.Models.Cinema>>();
            foreach (var cinemaHtml in listCinemaHtml)
            {
                var id = cinemaHtml.Attributes.Contains("href") ? cinemaHtml.Attributes["href"].Value.Split('/')[2].Trim() : "";
                if (!string.IsNullOrEmpty(id) && !CinemaRepository.Contains(id))
                {
                    var cinemaCrawler = new Model.Models.Cinema();
                    cinemaCrawler.ID = id;
                    cinemaCrawler.LocationID = cinemaHtml.Attributes["class"].Value.Replace("list-group-item cinema-option city-", "").Trim();
                    cinemaCrawler.Name = cinemaHtml.QuerySelector("h4").InnerText.Trim();
                    var chain = _cinemaChainRepository.Get(c => cinemaCrawler.Name.Contains(c.Name));
                    if (chain != null)
                    {
                        cinemaCrawler.CinemaChainID = chain.ID;
                    }
                    else
                    {
                        cinemaCrawler.CinemaChainID = "khac";
                    }
                    var loadCinemaTask = new Task<Model.Models.Cinema>(() =>loadCinema(ref cinemaCrawler, cinemaHtml.Attributes["href"].Value));
                    loadCinemaTask.Start();
                    listTaskCinema.Add(loadCinemaTask);
                }
            }
            await Task.WhenAll(listTaskCinema);
            var listCinema = new List<Model.Models.Cinema>();
            foreach (var task in listTaskCinema)
            {
                listCinema.Add(task.Result);
            }
            //CinemaRepository.AddRange(listCinema);
            return listCinema;
        }

        private Model.Models.Cinema loadCinema(ref Model.Models.Cinema cinema, string url)
        {
            HtmlDocument document = _htmlWeb.Load(cinemaUrl.Replace("/rap", url));
            var addressAndPhone = document.DocumentNode.QuerySelectorAll("dl.dl-horizontal > dd").ToList();
            if (addressAndPhone.Count > 1)
            {
                cinema.Address = addressAndPhone[0].InnerText.Trim();
                cinema.PhoneNumber = addressAndPhone[1].InnerText.Trim();
            }
            try
            {
                cinema.LinkImage = document.DocumentNode.QuerySelector("div#dia-chi-rap > img").Attributes["src"].Value.Trim();
            }
            catch
            {
                cinema.LinkImage = "";
            }
            cinema.Intro = document.DocumentNode.QuerySelector("div.cinema-description").InnerText.Trim();
            try
            {
                var htmlMaps = document.DocumentNode.QuerySelector("button#cinema-map-show-btn");
                String urlMaps = htmlMaps.Attributes["data-url"].Value.ToString().Trim();
                String longitude = urlMaps.Substring(urlMaps.IndexOf("!2d") + 3, lengthSubstring(urlMaps, urlMaps.IndexOf("!2d") + 3));
                String latitude = urlMaps.Substring(urlMaps.IndexOf("!3d") + 3, lengthSubstring(urlMaps, urlMaps.IndexOf("!3d") + 3));
                cinema.Longitude = decimal.Parse(longitude);
                cinema.Latitude = decimal.Parse(latitude);
            }
            catch (Exception)
            {
                //var htmlMaps = document.DocumentNode.QuerySelector("button#cinema-map-show-btn");
                //String urlMaps = htmlMaps.Attributes["data-url"].Value.ToString().Trim();
                //var longlat = urlMaps.Substring(urlMaps.IndexOf("sll=") + 4, lengthSubstring(urlMaps, urlMaps.IndexOf("sll=")))
                //String longitude = urlMaps.Substring(urlMaps.IndexOf("!2d") + 3, lengthSubstring(urlMaps, urlMaps.IndexOf("!2d") + 3));
                //String latitude = urlMaps.Substring(urlMaps.IndexOf("!3d") + 3, lengthSubstring(urlMaps, urlMaps.IndexOf("!3d") + 3));
                //cinema.Longitude = decimal.Parse(longitude);
                //cinema.Latitude = decimal.Parse(latitude);
            }
            new CinemaRepository(new DbFactory()).Add(cinema);
            return cinema;
        }

        private int lengthSubstring(String str, int startAt)
        {
            for (int i = 1; i < str.Length - startAt - 1; i++)
            {
                try
                {
                    Double.Parse(str.Substring(startAt, i));
                }
                catch (Exception)
                {
                    return i - 1;
                }
            }
            return str.Length - startAt - 1;
        }
    }
}