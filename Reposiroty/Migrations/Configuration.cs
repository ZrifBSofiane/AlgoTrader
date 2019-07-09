namespace Reposiroty.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Reposiroty.Config;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Reposiroty.AlgoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Reposiroty.AlgoDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new AlgoDbContext()));

            for(int i=0;i<4;i++)
            {
                var user  = new ApplicationUser()
                {
                    UserName = string.Format("User{0]", i.ToString())
                };
                manager.Create(user, string.Format("Password{0}", i.ToString()));
            }
        }
    }
}

