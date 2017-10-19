using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Cinema.Data.Infrastructure;
using Cinema.Web.Crawler;

namespace Cinema.Web.Models.QuartzJob
{
    public class SimpleJob
    {
        public IJob Job { get; set; }
        public ITrigger Trigger { get; set; }

        public SimpleJob(IJob job, ITrigger trigger)
        {
            Job = job;
            Trigger = trigger;
        }
    }

    public class RepeatJob : IJob
    {
        private static ErrorRepository _errorRepository = new ErrorRepository(new DbFactory());
        private static CrawlerData _crawlerData = new CrawlerData();

        //public SimpleJob(ErrorRepository errorRepository)
        //{
        //    _errorRepository = errorRepository;
        //}

        public async Task Execute(IJobExecutionContext context)
        {
            var error = new Error()
            {
                CreatedDate = DateTime.Now,
                Message = "Quartz Repeat Job",
            };
            _errorRepository.Add(error);

            await _crawlerData.Locations.Crawler();
        }
    }

    public class ScheduleJob : IJob
    {
        private static ErrorRepository _errorRepository = new ErrorRepository(new DbFactory());

        //public SimpleJob(ErrorRepository errorRepository)
        //{
        //    _errorRepository = errorRepository;
        //}

        public Task Execute(IJobExecutionContext context)
        {
            var error = new Error()
            {
                CreatedDate = DateTime.Now,
                Message = "Quartz Schedule Job",
            };
            _errorRepository.Add(error);
            return Task.FromResult<object>(null);
        }
    }
}