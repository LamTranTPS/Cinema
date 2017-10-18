namespace Cinema.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateerrortable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Errors", newName: "Error");
            AlterColumn("dbo.Error", "Message", c => c.String(maxLength: 200));
            AlterColumn("dbo.Error", "StackTrace", c => c.String(maxLength: 500));
            AlterColumn("dbo.Error", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Error", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Error", "StackTrace", c => c.String());
            AlterColumn("dbo.Error", "Message", c => c.String());
            RenameTable(name: "dbo.Error", newName: "Errors");
        }
    }
}
