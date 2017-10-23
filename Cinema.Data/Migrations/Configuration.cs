namespace Cinema.Data.Migrations
{
    using Cinema.Data.Infrastructure;
    using Cinema.Data.Repositories;
    using Cinema.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CinemaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CinemaDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var manager = new UserManager<ApplicationUser, int>(new UserStore<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>(context));

            var roleManager = new RoleManager<Role, int>(new RoleStore<Role, int, UserRole>(context));

            //var listCinemaChain = new List<CinemaChain>();
            //var listLocation = new List<Location>();
            //var listCinema = new List<Cinema>();


            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new Role { Name = "Admin" });
                roleManager.Create(new Role { Name = "Super Admin" });
                roleManager.Create(new Role { Name = "User" });
            }

            for (int i = 0; i < 10; i++)
            {
                manager.Create(new ApplicationUser()
                {
                    UserName = "admin." + i + "@cinema.com",
                    Email = "admin." + i + "@cinema.com",
                    Hometown = "Admin." + i,
                    EmailConfirmed = true,
                }, "cinema");

                manager.Create(new ApplicationUser()
                {
                    UserName = "User." + i,
                    Email = "user." + i + "@cinema.com",
                    Hometown = "user." + i + "@cinema.com",
                    EmailConfirmed = true,
                }, "cinema");

                //listCinemaChain.Add(new CinemaChain() { ID = i.ToString(), Name = "Cinema Chain " + i });
                //listLocation.Add(new Location() { ID = i.ToString(), Name = "Location " + i });
                //listCinema.Add(new Cinema() { ID = i, Name = "Cinema " + i, CinemaChainID = i.ToString(), LocationID = i.ToString() });

                var admin = manager.FindByEmail("admin." + i + "@cinema.com");

                manager.AddToRoles(admin.Id, new string[] { "Admin", "User" });
            }

            //context.CinemaChains.AddRange(listCinemaChain);
            //context.Locations.AddRange(listLocation);
            //context.Cinemas.AddRange(listCinema);

            var adminUser = manager.FindByEmail("admin.0@cinema.com");
            manager.AddToRoles(adminUser.Id, new string[] { "Super Admin", "Admin", "User" });

            context.CinemaChains.Add(new CinemaChain() { ID = "khac", Name = "Khác" });
            context.QuartzJobs.AddRange(new QuartzJob[] {
                new QuartzJob(){Name = "Crawler Cinema", Action = "Cinema.Crawler.Models.CrawlerCinemaJob, Cinema.Crawler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"},
                new QuartzJob(){Name = "Crawler Film", Action = "Cinema.Crawler.Models.CrawlerFilmJob, Cinema.Crawler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"},
                new QuartzJob(){Name = "Crawler All", Action = "Cinema.Crawler.Models.CrawlerJob, Cinema.Crawler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"},
                new QuartzJob(){Name = "Simple Job", Action = "Cinema.Crawler.Models.SimpleJob, Cinema.Crawler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"}
            });
            base.Seed(context);
        }
    }
}
