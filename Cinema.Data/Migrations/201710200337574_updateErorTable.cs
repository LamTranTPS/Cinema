namespace Cinema.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateErorTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Error", "Action", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Error", "Action");
        }
    }
}
