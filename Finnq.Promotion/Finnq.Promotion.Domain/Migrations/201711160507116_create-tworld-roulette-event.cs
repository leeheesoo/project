namespace Finnq.Promotion.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtworldrouletteevent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TWorldRouletteEventEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Channel = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 13, storeType: "nvarchar"),
                        PrizeType = c.Int(),
                        IpAddress = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TWorldRouletteEventInstantLotteryPrizeSettings",
                c => new
                    {
                        Date = c.DateTime(nullable: false, precision: 0),
                        PrizeType = c.Int(nullable: false),
                        Rate = c.Single(nullable: false),
                        TotalCount = c.Int(nullable: false),
                        WinnerCount = c.Int(nullable: false),
                        StartTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Date, t.PrizeType });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TWorldRouletteEventInstantLotteryPrizeSettings");
            DropTable("dbo.TWorldRouletteEventEntries");
        }
    }
}
