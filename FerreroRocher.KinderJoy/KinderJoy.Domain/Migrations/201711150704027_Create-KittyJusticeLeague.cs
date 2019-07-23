namespace KinderJoy.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateKittyJusticeLeague : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KittyJusticeLeagueInstantLotteries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Channel = c.Int(nullable: false),
                        PrizeType = c.Int(nullable: false),
                        IpAddress = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        Name = c.String(maxLength: 10, storeType: "nvarchar"),
                        Mobile = c.String(maxLength: 15, storeType: "nvarchar"),
                        ZipCode = c.String(maxLength: 5, storeType: "nvarchar"),
                        Address = c.String(maxLength: 255, storeType: "nvarchar"),
                        AddressDetail = c.String(maxLength: 255, storeType: "nvarchar"),
                        UpdateDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KittyJusticeLeagueInstantLotteryPrizeSettings",
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
            DropTable("dbo.KittyJusticeLeagueInstantLotteryPrizeSettings");
            DropTable("dbo.KittyJusticeLeagueInstantLotteries");
        }
    }
}
