namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinstanceTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InstanceTasks", "TaskTypeId", c => c.Int());
            DropColumn("dbo.InstanceTasks", "TaskTypeGuid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InstanceTasks", "TaskTypeGuid", c => c.Guid());
            DropColumn("dbo.InstanceTasks", "TaskTypeId");
        }
    }
}
