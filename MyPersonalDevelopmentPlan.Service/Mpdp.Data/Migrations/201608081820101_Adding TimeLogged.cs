namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTimeLogged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goal", "TimeLoggedTicks", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goal", "TimeLoggedTicks");
        }
    }
}
