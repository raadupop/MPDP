namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfileUpdate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfile", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "UserProfileId", "dbo.UserProfile");
            DropIndex("dbo.User", new[] { "UserProfileId" });
            DropIndex("dbo.UserProfile", new[] { "UserId" });
            DropColumn("dbo.User", "UserProfileId");
            DropTable("dbo.UserProfile");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "UserProfileId", c => c.Int());
            CreateIndex("dbo.UserProfile", "UserId");
            CreateIndex("dbo.User", "UserProfileId");
            AddForeignKey("dbo.User", "UserProfileId", "dbo.UserProfile", "Id");
            AddForeignKey("dbo.UserProfile", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
