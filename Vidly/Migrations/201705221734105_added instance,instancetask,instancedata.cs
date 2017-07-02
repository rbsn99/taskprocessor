namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedinstanceinstancetaskinstancedata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InstanceDatas",
                c => new
                    {
                        InstanceDataId = c.Int(nullable: false, identity: true),
                        InstanceDataGuid = c.Guid(nullable: false),
                        ItaskGuid = c.Guid(nullable: false),
                        TaskGuid = c.Guid(nullable: false),
                        DataLabel = c.String(),
                        DataValue = c.String(),
                    })
                .PrimaryKey(t => t.InstanceDataId);
            
            CreateTable(
                "dbo.Instances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstanceGuid = c.Guid(nullable: false),
                        InstanceName = c.String(),
                        InstanceState = c.String(),
                        LastMilestone = c.String(),
                        LastMilestoneDate = c.DateTime(),
                        RequesterId = c.Int(),
                        ProcessId = c.Int(),
                        ProcessGuid = c.Guid(),
                        ProcessName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        MyProperty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InstanceTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItaskGuid = c.Guid(nullable: false),
                        InstanceGuid = c.Guid(nullable: false),
                        ItaskState = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        TaskGuid = c.Guid(nullable: false),
                        TaskTypeGuid = c.Guid(),
                        TaskName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InstanceTasks");
            DropTable("dbo.Instances");
            DropTable("dbo.InstanceDatas");
        }
    }
}
