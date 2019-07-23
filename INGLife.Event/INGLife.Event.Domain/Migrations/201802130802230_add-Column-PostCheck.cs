namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnPostCheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KidsNoteEntries", "PostCheck", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KidsNoteEntries", "PostCheck");
        }
    }
}
