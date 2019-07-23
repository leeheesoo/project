namespace Samsonite.Mall.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTwoYearAnniversary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TwoYearAnniversaryEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SamsoniteId = c.String(nullable: false, maxLength: 250, storeType: "nvarchar"),
                        SamsoniteEncryptionId = c.String(nullable: false, maxLength: 250, storeType: "nvarchar"),
                        PrizeType = c.Int(nullable: false),
                        Channel = c.Int(nullable: false),
                        IpAddress = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TwoYearAnniversaryInstantPrizeSettings",
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
            
            CreateTable(
                "dbo.TwoYearAnniversaryWinCoupons",
                c => new
                    {
                        CouponType = c.Int(nullable: false),
                        CouponCode = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        TwoYearAnniversaryEntryId = c.Long(),
                        WinnerDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.CouponCode)
                .ForeignKey("dbo.TwoYearAnniversaryEntries", t => t.TwoYearAnniversaryEntryId)
                .Index(t => t.TwoYearAnniversaryEntryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TwoYearAnniversaryWinCoupons", "TwoYearAnniversaryEntryId", "dbo.TwoYearAnniversaryEntries");
            DropIndex("dbo.TwoYearAnniversaryWinCoupons", new[] { "TwoYearAnniversaryEntryId" });
            DropTable("dbo.TwoYearAnniversaryWinCoupons");
            DropTable("dbo.TwoYearAnniversaryInstantPrizeSettings");
            DropTable("dbo.TwoYearAnniversaryEntries");
        }
    }
}
