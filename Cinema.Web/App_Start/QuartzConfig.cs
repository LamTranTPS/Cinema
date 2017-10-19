using Cinema.Web.Models.QuartzJob;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
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
                await CreateRepeatJob();
                await CreateScheduleJob();

            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        public static async Task CreateRepeatJob()
        {
            IJobDetail job = JobBuilder.Create<RepeatJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        public static async Task CreateScheduleJob()
        {
            IJobDetail job = JobBuilder.Create<ScheduleJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                 .WithCronSchedule("30 * * * * ?")
                .Build();
            await _scheduler.ScheduleJob(job, trigger);
        }

    }
}