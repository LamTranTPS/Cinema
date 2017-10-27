using Cinema.Crawler.Crawler;
using Quartz;
using System.Threading.Tasks;

namespace Cinema.Crawler.Models
{
    public class CrawlerCinemaJob : BaseJob, IJob
    {
        private static CrawlerData _crawlerData = new CrawlerData();

        public Task Execute(IJobExecutionContext context)
        {
            _crawlerData.CrawlerCinema();
            return Task.FromResult<object>(null);
        }
    }
}