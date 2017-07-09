namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtasktypeidtoinstancedata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InstanceDatas", "TaskTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InstanceDatas", "TaskTypeId");
        }
    }
}
