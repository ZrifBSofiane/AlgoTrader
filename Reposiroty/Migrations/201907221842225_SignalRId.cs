namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SignalRId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "SignalRId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "SignalRId");
        }
    }
}
