using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Cinema.Web.Startup))]

namespace Cinema.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
            ConfigAutofac(app);
            ConfigureAuth(app);
        }
    }
}