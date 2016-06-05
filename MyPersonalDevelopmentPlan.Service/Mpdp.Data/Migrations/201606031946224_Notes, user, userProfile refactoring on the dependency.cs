namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotesuseruserProfilerefactoringonthedependency : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Note", "ObjectiveId", "dbo.Objective");
            DropForeignKey("dbo.User", "UserProfileId", "dbo.UserProfile");
            DropIndex("dbo.Note", new[] { "ObjectiveId" });
            DropIndex("dbo.User", new[] { "UserProfileId" });
            AddColumn("dbo.Note", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Note", "UserProfileId", c => c.Int(nullable: false));
            AddColumn("dbo.Note", "Priority", c => c.Int(nullable: false));
            CreateIndex("dbo.Note", "UserProfileId");
            AddForeignKey("dbo.Note", "UserProfileId", "dbo.UserProfile", "Id", cascadeDelete: true);
            DropColumn("dbo.Note", "ObjectiveId");
            DropColumn("dbo.Note", "Title");
            DropColumn("dbo.Note", "Content");
            DropColumn("dbo.User", "UserProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "UserProfileId", c => c.Int());
            AddColumn("dbo.Note", "Content", c => c.String(nullable: false));
            AddColumn("dbo.Note", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Note", "ObjectiveId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Note", "UserProfileId", "dbo.UserProfile");
            DropIndex("dbo.Note", new[] { "UserProfileId" });
            DropColumn("dbo.Note", "Priority");
            DropColumn("dbo.Note", "UserProfileId");
            DropColumn("dbo.Note", "Name");
            CreateIndex("dbo.User", "UserProfileId");
            CreateIndex("dbo.Note", "ObjectiveId");
            AddForeignKey("dbo.User", "UserProfileId", "dbo.UserProfile", "Id");
            AddForeignKey("dbo.Note", "ObjectiveId", "dbo.Objective", "Id", cascadeDelete: true);
        }
    }
}
