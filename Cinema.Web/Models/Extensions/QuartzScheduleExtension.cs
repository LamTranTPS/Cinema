using AutoMapper;
using Cinema.Model.Models;
using Cinema.Web.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.Extensions
{
    public static class QuartzScheduleExtension
    {
        public static QuartzScheduleViewModel ToViewModel(this QuartzSchedule self)
        {
            return Mapper.Map<QuartzScheduleViewModel>(self);
        }

        public static IEnumerable<QuartzScheduleViewModel> ToViewModel(this IEnumerable<QuartzSchedule> self)
        {
            return Mapper.Map<IEnumerable<QuartzScheduleViewModel>>(self);
        }

        public static QuartzSchedule ToEntityModel(this QuartzScheduleViewModel self)
        {
            return Mapper.Map<QuartzSchedule>(self);
        }

        public static IEnumerable<QuartzSchedule> ToEntityModel(this IEnumerable<QuartzScheduleViewModel> self)
        {
            return Mapper.Map<IEnumerable<QuartzSchedule>>(self);
        }
    }
}