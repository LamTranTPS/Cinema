using Cinema.Crawler.Crawler;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Cinema.Web.Models.QuartzJob
{
    public class CrawlerJob : IJob
    {
        private static CrawlerData _crawlerData = new CrawlerData();

        public async Task Execute(IJobExecutionContext context)
        {
            await _crawlerData.CrawlerAsync();
        }
    }
}