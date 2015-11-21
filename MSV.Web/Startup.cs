using ADSoft.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartupAttribute(typeof(ADSoft.Web.Startup))]
namespace ADSoft.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }

    }
} 