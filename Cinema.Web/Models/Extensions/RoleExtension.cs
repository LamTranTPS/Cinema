using AutoMapper;
using Cinema.Web.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.Extensions
{
    public static class RoleExtension
    {
        public static RoleViewModel ToViewModel(this IdentityRole self)
        {
            return Mapper.Map<RoleViewModel>(self);
        }

        public static IEnumerable<RoleViewModel> ToViewModel(this IEnumerable<IdentityRole> self)
        {
            return Mapper.Map<IEnumerable<RoleViewModel>>(self);
        }

        public static IdentityRole ToEntityModel(this RoleViewModel self)
        {
            return Mapper.Map<IdentityRole>(self);
        }

        public static IEnumerable<IdentityRole> ToEntityModel(this IEnumerable<RoleViewModel> self)
        {
            return Mapper.Map<IEnumerable<IdentityRole>>(self);
        }
    }
}