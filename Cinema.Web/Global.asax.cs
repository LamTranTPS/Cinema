using Cinema.Web.ActionFilters;
using Cinema.Crawler.Crawler;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;

namespace Cinema.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalFilters.Filters.Add(new AdminLog());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Register();

            QuartzConfig.RegisterAsync().Wait();

            new Thread(Crawler).Start();
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
            //    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //GlobalConfiguration.Configuration.Formatters
            //    .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }

        public async void Crawler()
        {
            await new CrawlerData().CrawlerAsync();
        }
    }
}
