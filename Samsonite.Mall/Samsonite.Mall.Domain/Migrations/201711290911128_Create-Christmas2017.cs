namespace Samsonite.Mall.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateChristmas2017 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Christmas2017Entry",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SamsoniteMallId = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        ChristmasBagType = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 13, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        IpAddress = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        Channel = c.String(nullable: false, maxLength: 6, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Christmas2017Entry");
        }
    }
}
