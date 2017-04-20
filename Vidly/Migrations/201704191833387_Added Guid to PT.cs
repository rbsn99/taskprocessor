namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGuidtoPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessTasks", "ProcessTaskGuid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProcessTasks", "ProcessTaskGuid");
        }
    }
}
