namespace Piccolo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RollbackData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShortUrls", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ShortUrls", new[] { "ApplicationUserId" });
            DropColumn("dbo.ShortUrls", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShortUrls", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShortUrls", "ApplicationUserId");
            AddForeignKey("dbo.ShortUrls", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
