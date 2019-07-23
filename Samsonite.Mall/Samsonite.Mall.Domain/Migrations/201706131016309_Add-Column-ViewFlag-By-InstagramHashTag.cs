namespace Samsonite.Mall.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnViewFlagByInstagramHashTag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BagtotheFutureEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        IdeaName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        IdeaDescription = c.String(nullable: false, maxLength: 1000, storeType: "nvarchar"),
                        File = c.String(maxLength: 300, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 13, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        IpAddress = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        Channel = c.String(nullable: false, maxLength: 6, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BagtotheFutureSns",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        SnsType = c.Int(nullable: false),
                        SnsId = c.String(maxLength: 100, storeType: "nvarchar"),
                        SnsName = c.String(maxLength: 100, storeType: "nvarchar"),
                        Post = c.String(maxLength: 300, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BagtotheFutureSnsUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BagtotheFutureSnsUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
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
            DropForeignKey("dbo.BagtotheFutureSns", "UserId", "dbo.BagtotheFutureSnsUsers");
            DropIndex("dbo.BagtotheFutureSns", new[] { "UserId" });
            DropTable("dbo.BagtotheFutureSnsUsers");
            DropTable("dbo.BagtotheFutureSns");
            DropTable("dbo.BagtotheFutureEntries");
        }
    }
}
