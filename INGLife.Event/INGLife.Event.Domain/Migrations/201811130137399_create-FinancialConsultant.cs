namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createFinancialConsultant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FinancialConsultantEntries",
                c => new
                    {
                        FcCode = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.FcCode);
            
            CreateTable(
                "dbo.FinancialConsultantNewCustomerEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Gender = c.String(nullable: false),
                        BirthDay = c.String(nullable: false),
                        PhoneCheck = c.Boolean(),
                        MessageCheck = c.Boolean(),
                        PostCheck = c.Boolean(),
                        EmailCheck = c.Boolean(),
                        RetentionPeriodNewType = c.Int(),
                        Email = c.String(),
                        ZipCode = c.String(maxLength: 5),
                        Address = c.String(maxLength: 255),
                        AddressDetail = c.String(maxLength: 255),
                        FcCode = c.String(nullable: false, maxLength: 15),
                        ProposerName = c.String(nullable: false, maxLength: 15),
                        OriginCustomerRandomNum = c.String(nullable: false, maxLength: 15),
                        IsMessage = c.Boolean(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(nullable: false, maxLength: 13),
                        CreateDate = c.DateTime(nullable: false),
                        IpAddress = c.String(nullable: false, maxLength: 15),
                        Channel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FinancialConsultantOriginalCustomerEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Gender = c.String(nullable: false),
                        BirthDay = c.String(nullable: false),
                        PhoneCheck = c.Boolean(),
                        MessageCheck = c.Boolean(),
                        PostCheck = c.Boolean(),
                        EmailCheck = c.Boolean(),
                        RetentionPeriodOriginType = c.Int(),
                        Email = c.String(),
                        ZipCode = c.String(maxLength: 5),
                        Address = c.String(maxLength: 255),
                        AddressDetail = c.String(maxLength: 255),
                        FcCode = c.String(nullable: false, maxLength: 15),
                        CustomerRandomNum = c.String(nullable: false, maxLength: 15),
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
            DropTable("dbo.FinancialConsultantOriginalCustomerEntries");
            DropTable("dbo.FinancialConsultantNewCustomerEntries");
            DropTable("dbo.FinancialConsultantEntries");
        }
    }
}
