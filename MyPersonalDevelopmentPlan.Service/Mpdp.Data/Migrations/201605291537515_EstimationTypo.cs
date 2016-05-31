namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstimationTypo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goal", "RemainingEstimatesTicks", c => c.Long(nullable: false));
            AddColumn("dbo.Objective", "RemaningEstimatesTicks", c => c.Long(nullable: false));
            DropColumn("dbo.Goal", "RemainingEstimationTicks");
            DropColumn("dbo.Objective", "RemaningEstimationTicks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Objective", "RemaningEstimationTicks", c => c.Long(nullable: false));
            AddColumn("dbo.Goal", "RemainingEstimationTicks", c => c.Long(nullable: false));
            DropColumn("dbo.Objective", "RemaningEstimatesTicks");
            DropColumn("dbo.Goal", "RemainingEstimatesTicks");
        }
    }
}
