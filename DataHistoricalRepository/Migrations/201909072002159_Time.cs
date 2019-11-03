namespace DataHistoricalRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Time : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoricalData", "Time", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistoricalData", "Time");
        }
    }
}
