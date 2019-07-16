namespace Reposiroty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameForex : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Forexes", newName: "Forex");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Forex", newName: "Forexes");
        }
    }
}
