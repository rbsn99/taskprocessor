namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateprocesstasks : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT ProcessTasks ON");
            Sql("insert into ProcessTasks (Id , TaskName,CreatedDate, ProcessTaskGuid, ProcessGuid, TaskTypeId) values(1,'Process starter', '" + DateTime.Now + "','" + Guid.NewGuid() + "','509e8339-cb36-4ec5-a35a-e1e2f2a3e233', 5)");
            Sql("insert into ProcessTasks (Id , TaskName,CreatedDate, ProcessTaskGuid, ProcessGuid, TaskTypeId) values(2,'first milestone', '" + DateTime.Now + "','" + Guid.NewGuid() + "','509e8339-cb36-4ec5-a35a-e1e2f2a3e233', 1)");
            Sql("SET IDENTITY_INSERT ProcessTasks OFF");
        }
        
        public override void Down()
        {
        }
    }
}
