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
    public class CinemaChainCrawler: BaseCrawler<CinemaChain>
    {
        private const string cinemaChainUrl = "http://lichchieu.net/khuyen-mai";

        private CinemaChainRepository CinemaChainRepository { get { return (CinemaChainRepository)_repository; } }
        public CinemaChainCrawler()
            :base()
        {
            _repository = new CinemaChainRepository();
        }

        public new List<CinemaChain> CrawlerData()
        {
            return LoadCinemaChains();
        }

        public List<CinemaChain> LoadCinemaChains()
        {
            HtmlDocument document = _htmlWeb.Load(cinemaChainUrl);
            var listCinemaChain = new List<CinemaChain>();
            CinemaChain cinemaChainCrawler;
            var listCinemaChainHtml = document.DocumentNode.QuerySelectorAll("li > a.glide-to");
            foreach (var cinemaChainHtml in listCinemaChainHtml)
            {
                var id = cinemaChainHtml.Attributes.Contains("href") ? cinemaChainHtml.Attributes["href"].Value.Replace("#khuyen-mai-", "").Trim() : "";
                if (!string.IsNullOrEmpty(id) && !CinemaChainRepository.Contains(id))
                {
                    cinemaChainCrawler = new CinemaChain();
                    cinemaChainCrawler.ID = id;
                    cinemaChainCrawler.Name = cinemaChainHtml.InnerText.Trim();
                    listCinemaChain.Add(cinemaChainCrawler);
                }
            }
            CinemaChainRepository.AddRange(listCinemaChain);
            return listCinemaChain;
        }
    }
}