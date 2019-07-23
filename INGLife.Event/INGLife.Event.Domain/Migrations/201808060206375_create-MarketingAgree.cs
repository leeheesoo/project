namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createMarketingAgree : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MarketingAgreeEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Gender = c.String(nullable: false),
                        BirthDay = c.String(nullable: false),
                        RetentionPeriodType = c.Int(),
                        UpdateDate = c.DateTime(),
                        IsMessage = c.Boolean(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(nullable: false, maxLength: 13),
                        CreateDate = c.DateTime(nullable: false),
                        IpAddress = c.String(nullable: false, maxLength: 15),
                        Channel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MarketingAgreeEntries");
        }
    }
}
