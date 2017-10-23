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
    public static class QuartzJobExtension
    {
        public static QuartzJobViewModel ToViewModel(this QuartzJob self)
        {
            return Mapper.Map<QuartzJobViewModel>(self);
        }

        public static IEnumerable<QuartzJobViewModel> ToViewModel(this IEnumerable<QuartzJob> self)
        {
            return Mapper.Map<IEnumerable<QuartzJobViewModel>>(self);
        }

        public static QuartzJob ToEntityModel(this QuartzJobViewModel self)
        {
            return Mapper.Map<QuartzJob>(self);
        }

        public static IEnumerable<QuartzJob> ToEntityModel(this IEnumerable<QuartzJobViewModel> self)
        {
            return Mapper.Map<IEnumerable<QuartzJob>>(self);
        }
    }
}