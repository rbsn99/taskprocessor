namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterPTTchangedatetimefromfloattodatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProcessTaskTypes", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProcessTaskTypes", "CreatedDate", c => c.Double(nullable: false));
        }
    }
}
