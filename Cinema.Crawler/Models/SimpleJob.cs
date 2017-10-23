using Cinema.Data.Infrastructure;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Cinema.Crawler.Models
{
    
    public class SimpleJob : IJob
    {
        private static ErrorRepository _errorRepository = new ErrorRepository(new DbFactory());

        public Task Execute(IJobExecutionContext context)
        {
            var error = new Error()
            {
                CreatedDate = DateTime.Now,
                Message = "Quartz Simple Job",
            };
            _errorRepository.Add(error);
            return Task.FromResult<object>(null);
        }
    }
}