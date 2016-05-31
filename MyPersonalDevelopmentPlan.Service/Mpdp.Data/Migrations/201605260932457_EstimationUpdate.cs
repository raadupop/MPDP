namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstimationUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Goal", "Estimation", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Goal", "RemaniningEstimates", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Objective", "Estimation", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Objective", "RemainingEstimate", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Objective", "ExtraTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Objective", "ExtraTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Objective", "RemainingEstimate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Objective", "Estimation", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Goal", "RemaniningEstimates", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Goal", "Estimation", c => c.DateTime(nullable: false));
        }
    }
}
