namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfileUpdate : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.User", "UserProfileId", c => c.Int());
            CreateIndex("dbo.User", "UserProfileId");
            AddForeignKey("dbo.User", "UserProfileId", "dbo.UserProfile", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.UserProfile", "UserId", "dbo.User");
            DropIndex("dbo.UserProfile", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "UserProfileId" });
            DropColumn("dbo.User", "UserProfileId");
            DropTable("dbo.UserProfile");
        }
    }
}
