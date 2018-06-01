namespace Piccolo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShortUrls", "LookupCount", c => c.Int());
            AlterColumn("dbo.ShortUrls", "LastLookupDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShortUrls", "LastLookupDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ShortUrls", "LookupCount", c => c.Int(nullable: false));
        }
    }
}
