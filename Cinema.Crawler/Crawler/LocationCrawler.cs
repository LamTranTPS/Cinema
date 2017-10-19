using Cinema.Data;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cinema.Web.Crawler
{
    public class LocationCrawler
    {
        private HtmlWeb _htmlWeb;
        private ILocationRepository _locationRepository;
        private string cinemaUrl = "http://lichchieu.net/rap";

        public LocationCrawler(HtmlWeb htmlWeb, ILocationRepository locationRepository)
        {
            _htmlWeb = htmlWeb;
            _locationRepository = locationRepository;
        }

        public Task<List<Location>> Crawler()
        {
            HtmlDocument document = _htmlWeb.Load(cinemaUrl);
            var listLocation = new List<Location>();
            Location locationCrawler;
            var listLocationHtml = document.DocumentNode.QuerySelectorAll("select.form-control#cinema-by-city>option");
            foreach(var locationHtml in listLocationHtml)
            {
                var id = locationHtml.Attributes["value"].Value.Trim();
                if (!string.IsNullOrEmpty(id) && !_locationRepository.Contains(id))
                {
                    locationCrawler = new Location();
                    locationCrawler.ID = id;
                    locationCrawler.Name = locationHtml.InnerText.Trim();
                    listLocation.Add(locationCrawler);
                }
            }
            return Task.FromResult<List<Location>>(listLocation);
        }
    }
}