namespace Piccolo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserToUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShortUrls", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShortUrls", "ApplicationUser_Id");
            AddForeignKey("dbo.ShortUrls", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShortUrls", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ShortUrls", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ShortUrls", "ApplicationUser_Id");
        }
    }
}
