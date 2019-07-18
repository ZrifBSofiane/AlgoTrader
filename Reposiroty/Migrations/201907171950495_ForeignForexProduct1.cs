namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignForexProduct1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Forex_Id", c => c.Int());
            CreateIndex("dbo.Products", "Forex_Id");
            AddForeignKey("dbo.Products", "Forex_Id", "dbo.Forex", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Forex_Id", "dbo.Forex");
            DropIndex("dbo.Products", new[] { "Forex_Id" });
            DropColumn("dbo.Products", "Forex_Id");
        }
    }
}
