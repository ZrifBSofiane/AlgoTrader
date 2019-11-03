namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParameterUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parameters", "Key", c => c.String());
            AddColumn("dbo.Parameters", "Value", c => c.String());
            DropColumn("dbo.Parameters", "IsMarketOpened");
            DropColumn("dbo.Parameters", "RequiredMargin");
            DropColumn("dbo.Parameters", "StartAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parameters", "StartAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Parameters", "RequiredMargin", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Parameters", "IsMarketOpened", c => c.Boolean(nullable: false));
            DropColumn("dbo.Parameters", "Value");
            DropColumn("dbo.Parameters", "Key");
        }
    }
}
