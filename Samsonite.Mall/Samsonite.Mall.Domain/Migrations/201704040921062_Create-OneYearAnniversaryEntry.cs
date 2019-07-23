namespace Samsonite.Mall.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOneYearAnniversaryEntry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OneYearAnniversaryEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AcrosticPoemFirst = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        AcrosticPoemSecond = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        AcrosticPoemThird = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        AcrosticPoemFourth = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        AcrosticPoemFifth = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        CongratulationMessage = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        SamsoniteId = c.String(maxLength: 50, storeType: "nvarchar"),
                        Mobile = c.String(maxLength: 11, storeType: "nvarchar"),
                        Channel = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        IpAddress = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        IsShow = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OneYearAnniversaryEntries");
        }
    }
}
