namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstimationTicksForObjective : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objective", "EstimationTicks", c => c.Long(nullable: false));
            AddColumn("dbo.Objective", "RemaningEstimationTicks", c => c.Long(nullable: false));
            AddColumn("dbo.Objective", "ExtraTimeTicks", c => c.Long(nullable: false));
            DropColumn("dbo.Objective", "Estimation");
            DropColumn("dbo.Objective", "RemainingEstimate");
            DropColumn("dbo.Objective", "ExtraTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Objective", "ExtraTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Objective", "RemainingEstimate", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Objective", "Estimation", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Objective", "ExtraTimeTicks");
            DropColumn("dbo.Objective", "RemaningEstimationTicks");
            DropColumn("dbo.Objective", "EstimationTicks");
        }
    }
}
