namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateprocesstask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessTasks", "TaskTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.ProcessTasks", "TaskTypeGuid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProcessTasks", "TaskTypeGuid", c => c.Guid(nullable: false));
            DropColumn("dbo.ProcessTasks", "TaskTypeId");
        }
    }
}
