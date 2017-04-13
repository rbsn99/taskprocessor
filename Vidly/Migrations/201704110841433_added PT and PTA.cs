namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPTandPTA : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProcessTasks", "ProcessGuid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProcessTasks", "ProcessGuid", c => c.Guid(nullable: false));
        }
    }
}
