namespace Mpdp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserRole", new[] { "USerId" });
            CreateIndex("dbo.UserRole", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserRole", new[] { "UserId" });
            CreateIndex("dbo.UserRole", "USerId");
        }
    }
}
