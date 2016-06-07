namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkedLogschanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkedLog", "TimeWorkedTicks", c => c.Long(nullable: false));
            DropColumn("dbo.WorkedLog", "TimeWorked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkedLog", "TimeWorked", c => c.DateTime(nullable: false));
            DropColumn("dbo.WorkedLog", "TimeWorkedTicks");
        }
    }
}
