using AutoMapper;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Cinema.Web.Models.ViewModels;
using System.Collections.Generic;

namespace Cinema.Web.Models.Extensions
{
    public static class UserExtension
    {
        public static UserViewModel ToViewModel(this ApplicationUser self)
        {
            return Mapper.Map<UserViewModel>(self);
        }

        public static IEnumerable<UserViewModel> ToViewModel(this IEnumerable<ApplicationUser> self)
        {
            return Mapper.Map<IEnumerable<UserViewModel>>(self);
        }

        public static ApplicationUser ToEntityModel(this UserViewModel self)
        {
            return Mapper.Map<ApplicationUser>(self);
        }

        public static IEnumerable<ApplicationUser> ToEntityModel(this IEnumerable<UserViewModel> self)
        {
            return Mapper.Map<IEnumerable<ApplicationUser>>(self);
        }
    }
}