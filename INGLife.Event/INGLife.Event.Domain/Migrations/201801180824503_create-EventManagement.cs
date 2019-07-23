namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createEventManagement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventManagements",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AffiliatesId = c.Long(nullable: false),
                        EventName = c.String(nullable: false, maxLength: 50),
                        PagePath = c.String(nullable: false, maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Affiliates", t => t.AffiliatesId, cascadeDelete: true)
                .Index(t => t.AffiliatesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventManagements", "AffiliatesId", "dbo.Affiliates");
            DropIndex("dbo.EventManagements", new[] { "AffiliatesId" });
            DropTable("dbo.EventManagements");
        }
    }
}
