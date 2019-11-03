using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Reposiroty;
using System.ComponentModel;

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
                LoginPath = new PathString("/Authentication/Login"),
                LogoutPath = new PathString("/Authentication/Logout"),
            });

            //*******************************************
            //          OWIN Authentication          *
            //*******************************************
            app.CreatePerOwinContext<AlgoDbContext>(AlgoDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            //*******************************************
            //                SignalR                   *
            //*******************************************
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR(hubConfiguration);
        }
    }
}