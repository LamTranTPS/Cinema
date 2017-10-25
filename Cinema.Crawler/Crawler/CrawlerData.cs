using Cinema.Data.Infrastructure;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cinema.Crawler.Crawler
{
    public class CrawlerData
    {
        private LocationCrawler Locations;
        private CinemaChainCrawler CinemaChains;
        private CinemaCrawler Cinemas;
        private FilmCrawler Films;
        private EventCrawler Events;
        public CrawlerData()
        {
            Locations = new LocationCrawler();
            CinemaChains = new CinemaChainCrawler();
            Cinemas = new CinemaCrawler();
            Films = new FilmCrawler();
            Events = new EventCrawler();
        }
        public void CrawlerAll()
        {
            var watch = Stopwatch.StartNew();
            CrawlerCinema();
            Debug.WriteLine("Update Cinema done: " + watch.ElapsedMilliseconds);
            CrawlerEvent();
            Debug.WriteLine("Update Event done: " + watch.ElapsedMilliseconds);
            CrawlerFilm();
            Debug.WriteLine("Update Film done: " + watch.ElapsedMilliseconds);
            watch.Stop();
            Debug.WriteLine("Update data done: " + watch.ElapsedMilliseconds);
        }

        public void CrawlerCinema()
        {
            CrawlerLocationAndChain().Wait();
            Cinemas.CrawlerData();
        }

        public void CrawlerFilm()
        {
            Films.CrawlerData();
        }
        public void CrawlerEvent()
        {
            Events.CrawlerData();
        }

        public async Task CrawlerLocationAndChain()
        {
            var locationTask = new Task<List<Location>>(() => Locations.CrawlerData());
            var cinemaChainTask = new Task<List<CinemaChain>>(() => CinemaChains.CrawlerData());
            locationTask.Start();
            cinemaChainTask.Start();
            await Task.WhenAll(locationTask, cinemaChainTask);
        }
    }
}