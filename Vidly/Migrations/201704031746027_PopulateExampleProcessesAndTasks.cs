namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateExampleProcessesAndTasks : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Processes ON");
            Sql("insert into Processes (Id,ProcessGuid,ProcessName,CreatedDate) values(1,'" + Guid.NewGuid() + "','Process one', '" + DateTime.Now + "')");
            Sql("insert into Processes (Id,ProcessGuid,ProcessName,CreatedDate) values(2,'" + Guid.NewGuid() + "','Process two', '" + DateTime.Now + "')");
            Sql("insert into Processes (Id,ProcessGuid,ProcessName,CreatedDate) values(3,'" + Guid.NewGuid() + "','Process three', '" + DateTime.Now + "')");
            Sql("insert into Processes (Id,ProcessGuid,ProcessName,CreatedDate) values(4,'" + Guid.NewGuid() + "','Process four', '" + DateTime.Now + "')");
            Sql("SET IDENTITY_INSERT Processes OFF");

        }

        public override void Down()
        {
        }
    }
}
