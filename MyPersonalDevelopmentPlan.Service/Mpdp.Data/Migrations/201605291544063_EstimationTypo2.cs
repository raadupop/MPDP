namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstimationTypo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objective", "RemainingEstimatesTicks", c => c.Long(nullable: false));
            DropColumn("dbo.Objective", "RemaningEstimatesTicks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Objective", "RemaningEstimatesTicks", c => c.Long(nullable: false));
            DropColumn("dbo.Objective", "RemainingEstimatesTicks");
        }
    }
}
