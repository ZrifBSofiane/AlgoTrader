namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecimalPnl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "PnL", c => c.Decimal(nullable: false, precision: 18, scale: 7));
            AlterColumn("dbo.Transactions", "StartPrice", c => c.Decimal(nullable: false, precision: 18, scale: 7));
            AlterColumn("dbo.Transactions", "EndPrice", c => c.Decimal(nullable: false, precision: 18, scale: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "EndPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Transactions", "StartPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Transactions", "PnL");
        }
    }
}
