namespace INGLife.Event.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnTnkAdKey4050db : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OverFortyFiveDbEntries", "TnkAdKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OverFortyFiveDbEntries", "TnkAdKey");
        }
    }
}
