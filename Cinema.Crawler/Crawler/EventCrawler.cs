using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace Cinema.Crawler.Crawler
{
    public class EventCrawler : BaseCrawler<Event>
    {
        private const string eventUrl = "http://lichchieu.net/khuyen-mai";

        private EventRepository EventRepository { get { return (EventRepository)_repository; } }

        public EventCrawler()
            : base()
        {
            _repository = new EventRepository();
        }

        public new List<Event> CrawlerData()
        {
            return LoadEvents().Result;
        }

        private async Task<List<Event>> LoadEvents()
        {
            var listChainID = new CinemaChainRepository().GetAll().Select(c => c.ID);
            var listTaskEvent = new List<Task<List<Event>>>();
            foreach (var chianID in listChainID)
            {
                if (chianID != "khac")
                {
                    var loadEventTask = new Task<List<Event>>(() => LoadEventByChain(chianID).Result);
                    loadEventTask.Start();
                    listTaskEvent.Add(loadEventTask);
                }
            }
            await Task.WhenAll(listTaskEvent);
            var listEvent = new List<Event>();
            foreach (var task in listTaskEvent)
            {
                listEvent.AddRange(task.Result);
            }
            return listEvent;
        }

        private async Task<List<Event>> LoadEventByChain(string chainID)
        {
            HtmlDocument document = _htmlWeb.Load(eventUrl + "/" + chainID);
            var listEventHtml = document.DocumentNode.QuerySelectorAll("ul.list-unstyled > li > div").ToList();
            var listTaskEvent = new List<Task<Event>>();
            foreach (var eventHtml in listEventHtml)
            {
                string url = eventHtml.QuerySelector("div.fixed-ratio-content > a").Attributes["href"].Value.Trim();
                var id = int.Parse(url.Split('/')[3]);
                if (id >= 0 && !new EventRepository().Contains(id))
                {
                    var eventCrawler = new Event();
                    eventCrawler.ID = id;
                    eventCrawler.CinemaChainID = chainID;
                    eventCrawler.LinkImage = eventHtml.QuerySelector("img.lazyload").Attributes["data-src"].Value.Trim();
                    eventCrawler.Name = eventHtml.QuerySelector("h3.offer-title").InnerText.Trim();
                    eventCrawler.Time = eventHtml.QuerySelector("div.period").InnerText.Trim();
                    eventCrawler.Time = WebUtility.HtmlDecode(eventCrawler.Time);
                    if (eventCrawler.Time != null && !eventCrawler.Time.ToUpper().Contains("TỪ"))
                    {
                        int index = eventCrawler.Time.LastIndexOf(" ");
                        try
                        {
                            eventCrawler.EndTime = DateTime.ParseExact(eventCrawler.Time.Substring(eventCrawler.Time.LastIndexOf(" ") + 1), "dd/MM/yyyy", null);
                        }
                        catch
                        {
                            eventCrawler.EndTime = DateTime.ParseExact(eventCrawler.Time.Substring(eventCrawler.Time.LastIndexOf(" ") + 1), "d/M/yyyy", null);
                        }
                    }

                    if ((eventCrawler.EndTime >= DateTime.Now.ToUniversalTime().AddHours(7.0) || eventCrawler.EndTime == null) && !eventCrawler.Name.Contains("(Hết Hạn)"))
                    {
                        var loadEventTask = new Task<Event>(() => LoadEvent(eventCrawler, url));
                        loadEventTask.Start();
                        listTaskEvent.Add(loadEventTask);
                    }
                }
            }
            await Task.WhenAll(listTaskEvent);
            var listEvent = new List<Event>();
            foreach (var task in listTaskEvent)
            {
                listEvent.Add(task.Result);
            }
            return listEvent;
        }

        private Event LoadEvent(Event _event, string url)
        {
            HtmlDocument document = _htmlWeb.Load(eventUrl.Replace("/khuyen-mai", url));
            _event.Intro = document.DocumentNode.QuerySelector("div.offer-cnt").InnerText.Trim();
            new EventRepository().Add(_event);
            return _event;
        }
    }
}