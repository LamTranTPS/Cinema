using Cinema.Crawler.Crawler;
using Quartz;
using System.Threading.Tasks;

namespace Cinema.Crawler.Models
{
    public class CrawlerCinemaJob : IJob
    {
        private static CrawlerData _crawlerData = new CrawlerData();

        public Task Execute(IJobExecutionContext context)
        {
            return _crawlerData.CrawlerCinema();
        }
    }
}