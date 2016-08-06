namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Propertyrename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkedLog", "DateRecorded", c => c.DateTime(nullable: false));
            DropColumn("dbo.WorkedLog", "LogDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkedLog", "LogDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.WorkedLog", "DateRecorded");
        }
    }
}
