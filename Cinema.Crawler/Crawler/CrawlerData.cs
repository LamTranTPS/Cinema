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
        public CrawlerData()
        {
            Locations = new LocationCrawler();
            CinemaChains = new CinemaChainCrawler();
            Cinemas = new CinemaCrawler();
            Films = new FilmCrawler();
        }

        public void CrawlerFilm()
        {
            var watch = Stopwatch.StartNew();
            //CrawlerCinema().Wait();
            //Debug.WriteLine("Update Cinema done: " + watch.ElapsedMilliseconds);
            Films.CrawlerData();
            watch.Stop();
            Debug.WriteLine("Update data done: " + watch.ElapsedMilliseconds);
        }

        public async Task CrawlerCinema()
        {
            await CrawlerLocationAndChain();
            Cinemas.CrawlerData();
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