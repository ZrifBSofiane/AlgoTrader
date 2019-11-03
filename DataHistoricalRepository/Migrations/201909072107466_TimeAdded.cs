namespace DataHistoricalRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoricalData", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistoricalData", "DateAdded");
        }
    }
}
