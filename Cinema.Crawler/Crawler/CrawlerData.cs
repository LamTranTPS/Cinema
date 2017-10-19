using Cinema.Data.Infrastructure;
using Cinema.Data.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Cinema.Web.Crawler
{
    public class CrawlerData
    {
        private IDbFactory _dbFactory;
        private HtmlWeb _htmlWeb;
        private ILocationRepository _locationRepository;

        public LocationCrawler Locations;
        public CrawlerData()
        {
            _htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };
            _dbFactory = new DbFactory();
            _locationRepository = new LocationRepository(_dbFactory);

            Locations = new LocationCrawler(_htmlWeb, _locationRepository);
        }
    }
}