namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProcesstasksrelatedclassesaddedPTTransitionandPTRecipients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Processes", "DeletedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProcessTasks", "ProcessGuid", c => c.Guid(nullable: false));
            AddColumn("dbo.ProcessTasks", "TaskTypeGuid", c => c.Guid(nullable: false));
            AddColumn("dbo.ProcessTasks", "CompletionTask", c => c.Byte(nullable: false));
            AddColumn("dbo.ProcessTasks", "DeletedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ProcessTasks", "TaskType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProcessTasks", "TaskType", c => c.String());
            DropColumn("dbo.ProcessTasks", "DeletedDate");
            DropColumn("dbo.ProcessTasks", "CompletionTask");
            DropColumn("dbo.ProcessTasks", "TaskTypeGuid");
            DropColumn("dbo.ProcessTasks", "ProcessGuid");
            DropColumn("dbo.Processes", "DeletedDate");
        }
    }
}
