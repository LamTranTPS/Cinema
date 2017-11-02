using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Cinema.Data;
using Cinema.Data.Infrastructure;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Cinema.Web
{
    public partial class Startup
    {
        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers

            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<CinemaDbContext>().AsSelf().InstancePerRequest();

            //Asp.net Identity
            builder.RegisterType<UserStore>().As<IUserStore<ApplicationUser, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(ErrorRepository).Assembly)
                 .Where(t => t.Name.EndsWith("Repository"))
                 .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container); //Set the WebApi DependencyResolver
        }
    }
}