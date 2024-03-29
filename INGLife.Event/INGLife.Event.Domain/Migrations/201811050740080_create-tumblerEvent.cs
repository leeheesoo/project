namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtumblerEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TumblerEventEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Gender = c.String(nullable: false),
                        BirthDay = c.String(nullable: false),
                        PhoneCheck = c.Boolean(),
                        MessageCheck = c.Boolean(),
                        PostCheck = c.Boolean(),
                        EmailCheck = c.Boolean(),
                        RetentionPeriodType = c.Int(nullable: false),
                        Email = c.String(),
                        ZipCode = c.String(maxLength: 5),
                        Address = c.String(maxLength: 255),
                        AddressDetail = c.String(maxLength: 255),
                        HealthCheck = c.Boolean(),
                        SalaryCheck = c.Boolean(),
                        SavingCheck = c.Boolean(),
                        FundCheck = c.Boolean(),
                        EtcCheck = c.Boolean(),
                        EventType = c.String(),
                        IsMessage = c.Boolean(),
                        CompleteDate = c.DateTime(),
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
            DropTable("dbo.TumblerEventEntries");
        }
    }
}
