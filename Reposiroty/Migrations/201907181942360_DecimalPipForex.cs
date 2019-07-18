namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecimalPipForex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Forex", "Pip", c => c.Decimal(nullable: false, precision: 18, scale: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forex", "Pip", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
