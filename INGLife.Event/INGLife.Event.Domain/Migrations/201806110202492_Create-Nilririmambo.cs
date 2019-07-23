namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNilririmambo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NilririmanboEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Mobile = c.String(nullable: false, maxLength: 15),
                        InstagramId = c.String(nullable: false, maxLength: 15),
                        FcCode = c.String(nullable: false, maxLength: 15),
                        IpAddress = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NilririmanboEntries");
        }
    }
}
