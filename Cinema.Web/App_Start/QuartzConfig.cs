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
                //await CreateCrawlerDataJob();
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

        //public static async Task CreateCrawlerDataJob()
        //{
        //    IJobDetail job = JobBuilder.Create<CrawlerJob>().Build();

        //    ITrigger trigger = TriggerBuilder.Create()
        //         .WithCronSchedule("00 00 03/15 * * ?")
        //        .Build();
        //    await Scheduler.ScheduleJob(job, trigger);
        //}

        public static async Task CreateScheduleFromDB()
        {
            var listSchedule = new QuartzScheduleRepository().GetAll(true);
            foreach (var schedule in listSchedule)
            {
                await AddSchedule(schedule);
            }
        }

        public static async Task<bool> AddSchedule(QuartzSchedule schedule)
        {
            try
            {
                var typeJob = Type.GetType(schedule.Job.Action);
                IJobDetail job = JobBuilder.Create(typeJob)
                    .WithIdentity(schedule.ID.ToString())
                    .Build();
                job.JobDataMap["ScheduleID"] = schedule.ID;
                ITrigger trigger = TriggerBuilder.Create()
                    //.WithIdentity(schedule.ID.ToString())
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
                //await Scheduler.UnscheduleJob(new TriggerKey(scheduleID.ToString()));
                await Scheduler.DeleteJob(new JobKey(scheduleID.ToString()));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<bool> PauseJob(int scheduleID)
        {
            try
            {
                var jobKey = new JobKey(scheduleID.ToString());
                if (await Scheduler.CheckExists(jobKey))
                {
                    await Scheduler.PauseJob(jobKey);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<bool> StartJob(int scheduleID)
        {
            try
            {
                if (!await Scheduler.CheckExists(new JobKey(scheduleID.ToString()))){
                    var schedule = new QuartzScheduleRepository().Get(scheduleID);
                    return await AddSchedule(schedule);
                }
                await Scheduler.ResumeJob(new JobKey(scheduleID.ToString()));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}