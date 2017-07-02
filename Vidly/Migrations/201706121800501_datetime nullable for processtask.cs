namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimenullableforprocesstask : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProcessTasks", "DeletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProcessTasks", "DeletedDate", c => c.DateTime(nullable: false));
        }
    }
}
