﻿using Cinema.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Cinema.Data
{
    public class CinemaDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Film> Films { set; get; }
        public DbSet<Schedule> Schedules { set; get; }
        public DbSet<Model.Models.Cinema> Cinemas { set; get; }
        public DbSet<CinemaChain> CinemaChains { set; get; }
        public DbSet<Location> Locations { set; get; }
        public DbSet<Error> Errors { set; get; }

        public CinemaDbContext() : base("CinemaConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static CinemaDbContext Create()
        {
            return new CinemaDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}