namespace Cinema.Data.Migrations
{
    using Cinema.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
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

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CinemaDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CinemaDbContext()));



            for (int i = 0; i < 10; i++)
            {
                manager.Create(new ApplicationUser()
                {
                    UserName = "Admin." + i,
                    Email = "admin." + i + "@cinema.com",
                    Hometown = "admin." + i + "@cinema.com",
                    EmailConfirmed = true,
                }, "cinema");

                manager.Create(new ApplicationUser()
                {
                    UserName = "User." + i,
                    Email = "user." + i + "@cinema.com",
                    Hometown = "user." + i + "@cinema.com",
                    EmailConfirmed = true,
                }, "cinema");


                var admin = manager.FindByEmail("admin." + i + "@cinema.com");

                manager.AddToRoles(admin.Id, new string[] { "Admin", "User" });
            }

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Super Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("admin.0@cinema.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Super Admin", "Admin", "User" });
        }
    }
}
