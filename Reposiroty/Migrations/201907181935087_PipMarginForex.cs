namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PipMarginForex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forex", "Pip", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Forex", "MarginPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forex", "MarginPercentage");
            DropColumn("dbo.Forex", "Pip");
        }
    }
}
