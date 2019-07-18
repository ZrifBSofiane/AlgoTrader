namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignForexProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Forex", "Product_Id", "dbo.Products");
            DropIndex("dbo.Forex", new[] { "Product_Id" });
            DropColumn("dbo.Forex", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forex", "Product_Id", c => c.Int());
            CreateIndex("dbo.Forex", "Product_Id");
            AddForeignKey("dbo.Forex", "Product_Id", "dbo.Products", "Id");
        }
    }
}
