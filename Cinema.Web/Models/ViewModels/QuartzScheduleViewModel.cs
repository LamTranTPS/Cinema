using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.ViewModels
{
    public class QuartzScheduleViewModel
    {
        public int ID { set; get; }
        
        public string Name { set; get; }
        
        public string TimeExpression { set; get; }
        
        public int JobID { set; get; }
        
        public string JobName { set; get; }
    }
}