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
    public static class RoleExtension
    {
        public static RoleViewModel ToViewModel(this Role self)
        {
            return Mapper.Map<RoleViewModel>(self);
        }

        public static IEnumerable<RoleViewModel> ToViewModel(this IEnumerable<Role> self)
        {
            return Mapper.Map<IEnumerable<RoleViewModel>>(self);
        }

        public static Role ToEntityModel(this RoleViewModel self)
        {
            return Mapper.Map<Role>(self);
        }

        public static IEnumerable<Role> ToEntityModel(this IEnumerable<RoleViewModel> self)
        {
            return Mapper.Map<IEnumerable<Role>>(self);
        }
    }
}