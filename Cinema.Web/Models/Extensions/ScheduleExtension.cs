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
    public static class ScheduleExtension
    {
        public static ScheduleViewModel ToViewModel(this Schedule self)
        {
            return Mapper.Map<ScheduleViewModel>(self);
        }

        public static IEnumerable<ScheduleViewModel> ToViewModel(this IEnumerable<Schedule> self)
        {
            return Mapper.Map<IEnumerable<ScheduleViewModel>>(self);
        }

        public static Schedule ToEntityModel(this ScheduleViewModel self)
        {
            return Mapper.Map<Schedule>(self);
        }

        public static IEnumerable<Schedule> ToEntityModel(this IEnumerable<ScheduleViewModel> self)
        {
            return Mapper.Map<IEnumerable<Schedule>>(self);
        }
    }
}