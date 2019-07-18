namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Type");
        }
    }
}
