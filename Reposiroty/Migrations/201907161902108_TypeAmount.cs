namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeAmount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Amount", c => c.Long(nullable: false));
        }
    }
}
