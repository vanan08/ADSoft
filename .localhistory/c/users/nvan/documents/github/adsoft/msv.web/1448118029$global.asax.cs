using ADSoft.Web.Hubs;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ADSoft.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings["NORTHWNDContext"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterMiniProfiler();
            
            //Start SqlDependency with application initialization
            SqlDependency.Stop(connString);
            SqlDependency.Start(connString);

            //Register sqldependency
            DiscussMessageRepository discussMessageRepository = new DiscussMessageRepository();
            discussMessageRepository.RegisterDiscussMessages();
        }

        protected void Application_BeginRequest()
        {
            MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        private void RegisterMiniProfiler()
        {
            GlobalFilters.Filters.Add(new StackExchange.Profiling.Mvc.ProfilingActionFilter());
            var ignored = MiniProfiler.Settings.IgnoredPaths.ToList();
            ignored.Add("/__browserLink/");
            MiniProfiler.Settings.IgnoredPaths = ignored.ToArray();
        }
    }
}
