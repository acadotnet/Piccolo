namespace Piccolo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingHashColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShortUrls", "Hash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShortUrls", "Hash", c => c.String(nullable: false));
        }
    }
}
