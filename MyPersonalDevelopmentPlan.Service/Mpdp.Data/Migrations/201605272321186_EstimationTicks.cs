namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstimationTicks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goal", "EstimationTicks", c => c.Long(nullable: false));
            AddColumn("dbo.Goal", "RemainingEstimationTicks", c => c.Long(nullable: false));
            DropColumn("dbo.Goal", "Estimation");
            DropColumn("dbo.Goal", "RemaniningEstimates");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goal", "RemaniningEstimates", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Goal", "Estimation", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Goal", "RemainingEstimationTicks");
            DropColumn("dbo.Goal", "EstimationTicks");
        }
    }
}
