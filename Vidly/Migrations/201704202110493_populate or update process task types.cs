namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateorupdateprocesstasktypes : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT ProcessTaskTypes ON");
            Sql("insert into ProcessTaskTypes (Id ,ProcessTaskTypeName,CreatedDate) values(5,'Process Start', '" + DateTime.Now + "')");
            Sql("insert into ProcessTaskTypes (Id ,ProcessTaskTypeName,CreatedDate) values(6,'Process End', '" + DateTime.Now + "')");
            Sql("SET IDENTITY_INSERT ProcessTaskTypes OFF");
        }
        
        public override void Down()
        {
        }
    }
}
