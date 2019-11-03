namespace DataHistoricalRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoricalData",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Open = c.Decimal(nullable: false, precision: 18, scale: 7),
                        High = c.Decimal(nullable: false, precision: 18, scale: 7),
                        Low = c.Decimal(nullable: false, precision: 18, scale: 7),
                        Close = c.Decimal(nullable: false, precision: 18, scale: 7),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HistoricalData");
        }
    }
}
