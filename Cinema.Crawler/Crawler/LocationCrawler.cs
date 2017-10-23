using Cinema.Data;
using Cinema.Data.Infrastructure;
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

namespace Cinema.Crawler.Crawler
{
    public class LocationCrawler: BaseCrawler<Location>
    {
        private const string locationUrl = "http://lichchieu.net/rap";

        private LocationRepository LocationRepository { get { return (LocationRepository)_repository; } }

        public LocationCrawler()
            :base()
        {
            _repository = new LocationRepository();
        }

        public new List<Location> CrawlerData()
        {
            return LoadLocations();
        }

        public List<Location> LoadLocations()
        {
            HtmlDocument document = _htmlWeb.Load(locationUrl);
            var listLocation = new List<Location>();
            Location locationCrawler;
            var listLocationHtml = document.DocumentNode.QuerySelector("select#cinema-by-city");
            for(int i = 0; i < listLocationHtml.ChildNodes.Count; i ++)
            {
                if (listLocationHtml.ChildNodes[i].Name == "option" && listLocationHtml.ChildNodes[i].Attributes.Contains("value"))
                {
                    var id = (listLocationHtml.ChildNodes[i].Attributes["value"].Value ?? "").Trim();
                    if (!string.IsNullOrEmpty(id) && !LocationRepository.Contains(id))
                    {
                        i++;
                        locationCrawler = new Location();
                        locationCrawler.ID = id;
                        locationCrawler.Name = listLocationHtml.ChildNodes[i].InnerText.Trim();
                        listLocation.Add(locationCrawler);
                    }
                }
            }
            LocationRepository.AddRange(listLocation);
            return listLocation;
        }
    }
}