namespace Piccolo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMiddleName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MiddleName");
        }
    }
}
