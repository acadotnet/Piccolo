namespace Piccolo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingShortUrls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShortUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hash = c.String(nullable: false),
                        LongUrl = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LookupCount = c.Int(nullable: false),
                        LastLookupDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShortUrls");
        }
    }
}
