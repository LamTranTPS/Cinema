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
        public LocationCrawler Locations;
        public CinemaChainCrawler CinemaChains;
        public CinemaCrawler Cinemas;
        public CrawlerData()
        {
            Locations = new LocationCrawler();
            CinemaChains = new CinemaChainCrawler();
            Cinemas = new CinemaCrawler();
        }

        public async Task CrawlerAsync()
        {
            var watch = Stopwatch.StartNew();
            await CrawlerLocationAndChain();
            Debug.WriteLine("Update Location and chain done: " + watch.ElapsedMilliseconds);
            Cinemas.CrawlerData();
            watch.Stop();
            Debug.WriteLine("Update data done: " + watch.ElapsedMilliseconds);
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