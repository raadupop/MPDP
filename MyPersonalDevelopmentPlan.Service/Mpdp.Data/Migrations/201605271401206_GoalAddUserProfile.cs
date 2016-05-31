namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GoalAddUserProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Goal", "UserId", "dbo.User");
            DropIndex("dbo.Goal", new[] { "UserId" });
            AddColumn("dbo.Goal", "UserProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Goal", "UserProfileId");
            AddForeignKey("dbo.Goal", "UserProfileId", "dbo.UserProfile", "Id", cascadeDelete: true);
            DropColumn("dbo.Goal", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goal", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Goal", "UserProfileId", "dbo.UserProfile");
            DropIndex("dbo.Goal", new[] { "UserProfileId" });
            DropColumn("dbo.Goal", "UserProfileId");
            CreateIndex("dbo.Goal", "UserId");
            AddForeignKey("dbo.Goal", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
