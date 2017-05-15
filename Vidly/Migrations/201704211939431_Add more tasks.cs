namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addmoretasks : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT ProcessTasks ON");
            Sql("insert into ProcessTasks (Id , TaskName,CreatedDate, ProcessTaskGuid, ProcessGuid, TaskTypeId) values(3,'Genral mail notification', '" + DateTime.Now + "','" + Guid.NewGuid() + "','509e8339-cb36-4ec5-a35a-e1e2f2a3e233', 2)");
            Sql("insert into ProcessTasks (Id , TaskName,CreatedDate, ProcessTaskGuid, ProcessGuid, TaskTypeId) values(4,'Process closure', '" + DateTime.Now + "','" + Guid.NewGuid() + "','509e8339-cb36-4ec5-a35a-e1e2f2a3e233', 6)");
            Sql("SET IDENTITY_INSERT ProcessTasks OFF");

 
        }
        
        public override void Down()
        {
        }
    }
}
