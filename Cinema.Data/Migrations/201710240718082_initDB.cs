namespace Cinema.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CinemaChain",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cinema",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        LinkImage = c.String(maxLength: 200),
                        PhoneNumber = c.String(maxLength: 30),
                        Address = c.String(maxLength: 300),
                        Intro = c.String(maxLength: 4000),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        LocationID = c.String(nullable: false, maxLength: 20),
                        CinemaChainID = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CinemaChain", t => t.CinemaChainID, cascadeDelete: true)
                .ForeignKey("dbo.Location", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.LocationID)
                .Index(t => t.CinemaChainID);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 50),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Action = c.String(maxLength: 200),
                        Message = c.String(maxLength: 200),
                        StackTrace = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Time = c.String(maxLength: 50),
                        EndTime = c.DateTime(),
                        LinkImage = c.String(maxLength: 200),
                        Intro = c.String(maxLength: 4000),
                        CinemaChainID = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CinemaChain", t => t.CinemaChainID, cascadeDelete: true)
                .Index(t => t.CinemaChainID);
            
            CreateTable(
                "dbo.Film",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Premiere = c.DateTime(),
                        Time = c.String(maxLength: 50),
                        Genre = c.String(maxLength: 50),
                        LinkTrailer = c.String(maxLength: 200),
                        LinkPoster = c.String(maxLength: 200),
                        LinkImage = c.String(maxLength: 200),
                        Intro = c.String(maxLength: 4000),
                        Actor = c.String(maxLength: 100),
                        Director = c.String(maxLength: 200),
                        Country = c.String(maxLength: 50),
                        Classification = c.String(maxLength: 20),
                        IMDB = c.Decimal(precision: 18, scale: 2),
                        IsHot = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuartzJob",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Action = c.String(maxLength: 200),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuartzSchedule",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        TimeExpression = c.String(maxLength: 30),
                        Status = c.Boolean(nullable: false),
                        JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuartzJob", t => t.JobID, cascadeDelete: true)
                .Index(t => t.JobID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 30),
                        LinkTicket = c.String(maxLength: 200),
                        DateTime = c.DateTime(nullable: false),
                        Type = c.String(maxLength: 20),
                        CinemaID = c.Int(nullable: false),
                        FilmID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cinema", t => t.CinemaID, cascadeDelete: true)
                .ForeignKey("dbo.Film", t => t.FilmID, cascadeDelete: true)
                .Index(t => t.CinemaID)
                .Index(t => t.FilmID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hometown = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Schedule", "FilmID", "dbo.Film");
            DropForeignKey("dbo.Schedule", "CinemaID", "dbo.Cinema");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.QuartzSchedule", "JobID", "dbo.QuartzJob");
            DropForeignKey("dbo.Event", "CinemaChainID", "dbo.CinemaChain");
            DropForeignKey("dbo.Cinema", "LocationID", "dbo.Location");
            DropForeignKey("dbo.Cinema", "CinemaChainID", "dbo.CinemaChain");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Schedule", new[] { "FilmID" });
            DropIndex("dbo.Schedule", new[] { "CinemaID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.QuartzSchedule", new[] { "JobID" });
            DropIndex("dbo.Event", new[] { "CinemaChainID" });
            DropIndex("dbo.Cinema", new[] { "CinemaChainID" });
            DropIndex("dbo.Cinema", new[] { "LocationID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Schedule");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.QuartzSchedule");
            DropTable("dbo.QuartzJob");
            DropTable("dbo.Film");
            DropTable("dbo.Event");
            DropTable("dbo.Error");
            DropTable("dbo.Location");
            DropTable("dbo.Cinema");
            DropTable("dbo.CinemaChain");
        }
    }
}
