using AutoMapper;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Cinema.Web.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Web
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(config =>
            {
                //EntityModel to ViewModel
                config.CreateMap<IdentityRole, RoleViewModel>();
                config.CreateMap<ApplicationUser, UserViewModel>();

                //ViewModel to EntityModel
                config.CreateMap<RoleViewModel, IdentityRole>();
                config.CreateMap<UserViewModel, ApplicationUser>();
            });
        }
    }
}