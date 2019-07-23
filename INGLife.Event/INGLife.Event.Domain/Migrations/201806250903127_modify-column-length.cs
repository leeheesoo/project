namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifycolumnlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NilririmanboEntries", "InstagramId", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NilririmanboEntries", "InstagramId", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
