using AutoMapper;
using Cinema.Common.Extensions;
using Cinema.Model.Models;
using Cinema.Web.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

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
                config.CreateMap<Model.Models.Cinema, CinemaViewModel>().AfterMap((e,v) =>
                    v.Alias = e.Name.ToUnsignString());
                config.CreateMap<QuartzJob, QuartzJobViewModel>();
                config.CreateMap<QuartzSchedule, QuartzScheduleViewModel>();

                //ViewModel to EntityModel
                config.CreateMap<RoleViewModel, IdentityRole>();
                config.CreateMap<UserViewModel, ApplicationUser>();
                config.CreateMap<CinemaViewModel, Model.Models.Cinema>();
                config.CreateMap<QuartzJobViewModel, QuartzJob>();
                config.CreateMap<QuartzScheduleViewModel, QuartzSchedule>();
            });
        }
    }
}