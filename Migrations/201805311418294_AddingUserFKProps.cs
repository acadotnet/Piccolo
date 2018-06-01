namespace Piccolo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserFKProps : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ShortUrls", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.ShortUrls", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ShortUrls", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.ShortUrls", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
