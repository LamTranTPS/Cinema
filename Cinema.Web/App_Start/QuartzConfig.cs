using Cinema.Crawler.Models;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace Cinema.Web
{
    public class QuartzConfig
    {
        private static IScheduler _scheduler;

        public static IScheduler Scheduler { get { return _scheduler; } }

        public static async Task RegisterAsync()
        {
            try
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                _scheduler = await factory.GetScheduler();

                await _scheduler.Start();
                //await CreateRepeatJob();
                //await CreateScheduleJob();
                await CreateCrawlerDataJob();
                await CreateScheduleFromDB();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        //public static async Task CreateRepeatJob()
        //{
        //    IJobDetail job = JobBuilder.Create<RepeatJob>().Build();

        //    ITrigger trigger = TriggerBuilder.Create()
        //        .StartNow()
        //        .WithSimpleSchedule(x => x
        //            .WithIntervalInSeconds(30)
        //            .RepeatForever())
        //        .Build();

        //    await Scheduler.ScheduleJob(job, trigger);
        //}

        //public static async Task CreateScheduleJob()
        //{
        //    IJobDetail job = JobBuilder.Create<ScheduleJob>().Build();

        //    ITrigger trigger = TriggerBuilder.Create()
        //         .WithCronSchedule("30 * * * * ?")
        //        .Build();
        //    await Scheduler.ScheduleJob(job, trigger);
        //}

        public static async Task CreateCrawlerDataJob()
        {
            IJobDetail job = JobBuilder.Create<CrawlerJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                 .WithCronSchedule("00 00 03/15 * * ?")
                .Build();
            await Scheduler.ScheduleJob(job, trigger);
        }

        public static async Task CreateScheduleFromDB()
        {
            var listSchedule = new QuartzScheduleRepository().GetAll(new string[] { "Job" });
            foreach (var schedule in listSchedule)
            {
                await AddScheduleAsync(schedule);
            }
        }

        public static async Task<bool> AddScheduleAsync(QuartzSchedule schedule)
        {
            try
            {
                var typeJob = Type.GetType(schedule.Job.Action);
                IJobDetail job = JobBuilder.Create(typeJob).Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(schedule.ID.ToString())
                     .WithCronSchedule(schedule.TimeExpression)
                    .Build();
                await Scheduler.ScheduleJob(job, trigger);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<bool> DeleteJob(int scheduleID)
        {
            try
            {
                await Scheduler.DeleteJob(new JobKey(scheduleID.ToString()));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}