namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateKidsNote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KidsNoteEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Gender = c.String(nullable: false),
                        BirthDay = c.String(nullable: false),
                        ChildAge = c.Int(nullable: false),
                        ChildName = c.String(),
                        PhoneCheck = c.Boolean(),
                        MessageCheck = c.Boolean(),
                        EmailCheck = c.Boolean(),
                        RetentionPeriodType = c.Int(),
                        Email = c.String(),
                        ZipCode = c.String(maxLength: 5),
                        Address = c.String(maxLength: 255),
                        AddressDetail = c.String(maxLength: 255),
                        UpdateDate = c.DateTime(),
                        EventType = c.Int(nullable: false),
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
            DropTable("dbo.KidsNoteEntries");
        }
    }
}
