using Quartz;
using Quartz.Impl;

namespace MailingService
{
    public  class SchedularManager
    {
        private static readonly IScheduler scheduler = new StdSchedulerFactory().GetScheduler().Result;
        public static void StartSchedular()
        {
            scheduler.Start();
        }
    }
}
