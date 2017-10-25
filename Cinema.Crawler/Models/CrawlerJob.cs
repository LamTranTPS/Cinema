using Cinema.Crawler.Crawler;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cinema.Crawler.Models
{
    public class CrawlerJob : IJob
    {
        private static CrawlerData _crawlerData = new CrawlerData();

        public Task Execute(IJobExecutionContext context)
        {
            _crawlerData.CrawlerAll();
            return Task.FromResult<object>(null);
        }
    }
}