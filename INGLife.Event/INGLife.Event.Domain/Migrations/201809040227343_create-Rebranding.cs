namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createRebranding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RebrandingConsultingAgreeEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Gender = c.String(nullable: false),
                        BirthDay = c.String(nullable: false),
                        AttetionTypoe = c.Int(nullable: false),
                        ZipCode = c.String(maxLength: 5),
                        Address = c.String(maxLength: 255),
                        AddressDetail = c.String(maxLength: 255),
                        UpdateDate = c.DateTime(),
                        IsMessage = c.Boolean(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(nullable: false, maxLength: 13),
                        CreateDate = c.DateTime(nullable: false),
                        IpAddress = c.String(nullable: false, maxLength: 15),
                        Channel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RebrandingMarketingAgreeEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Gender = c.String(nullable: false),
                        BirthDay = c.String(nullable: false),
                        PhoneCheck = c.Boolean(nullable: false),
                        MessageCheck = c.Boolean(nullable: false),
                        EmailCheck = c.Boolean(nullable: false),
                        PostCheck = c.Boolean(nullable: false),
                        RetentionPeriodType = c.Int(nullable: false),
                        Email = c.String(),
                        ZipCode = c.String(maxLength: 5),
                        Address = c.String(maxLength: 255),
                        AddressDetail = c.String(maxLength: 255),
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
            DropTable("dbo.RebrandingMarketingAgreeEntries");
            DropTable("dbo.RebrandingConsultingAgreeEntries");
        }
    }
}
