using Cinema.Data.Infrastructure;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Cinema.Crawler.Models
{

    public class BaseJob
    {
        public int ScheduleID { get; set; }
    }
}