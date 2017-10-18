namespace Cinema.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
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
                        ID = c.String(nullable: false, maxLength: 20),
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
                "dbo.Film",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 50),
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
                        LocationID = c.String(maxLength: 20),
                        CinemaChainID = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 20),
                        LinkTicket = c.String(maxLength: 200),
                        Date = c.DateTime(nullable: false),
                        Time = c.String(nullable: false, maxLength: 20),
                        Type = c.String(maxLength: 20),
                        CinemaID = c.String(nullable: false, maxLength: 20),
                        FilmID = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cinema", t => t.CinemaID, cascadeDelete: true)
                .ForeignKey("dbo.Film", t => t.FilmID, cascadeDelete: true)
                .Index(t => t.CinemaID)
                .Index(t => t.FilmID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedule", "FilmID", "dbo.Film");
            DropForeignKey("dbo.Schedule", "CinemaID", "dbo.Cinema");
            DropForeignKey("dbo.Cinema", "LocationID", "dbo.Location");
            DropForeignKey("dbo.Cinema", "CinemaChainID", "dbo.CinemaChain");
            DropIndex("dbo.Schedule", new[] { "FilmID" });
            DropIndex("dbo.Schedule", new[] { "CinemaID" });
            DropIndex("dbo.Cinema", new[] { "CinemaChainID" });
            DropIndex("dbo.Cinema", new[] { "LocationID" });
            DropTable("dbo.Schedule");
            DropTable("dbo.Film");
            DropTable("dbo.Location");
            DropTable("dbo.Cinema");
            DropTable("dbo.CinemaChain");
        }
    }
}
