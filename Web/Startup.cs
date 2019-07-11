using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Reposiroty;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Web.Startup))]
namespace Web
{
    public class Startup
    {
        private IContainer container = new Container();


        public void Configuration(IAppBuilder app)
        {
            //*******************************************
            //          Account Authentication          *
            //*******************************************
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout"),
            });

            //*******************************************
            //          OWIN Authentication          *
            //*******************************************
            app.CreatePerOwinContext<AlgoDbContext>(AlgoDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
        }
    }
}