namespace Finnq.Promotion.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtmapevententry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TmapEventEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Phone = c.String(nullable: false, maxLength: 13, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        IpAddress = c.String(nullable: false, unicode: false),
                        IsMobileDevice = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TmapEventEntries");
        }
    }
}
