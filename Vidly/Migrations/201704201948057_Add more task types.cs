namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addmoretasktypes : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT ProcessTaskTypes ON");
            Sql("insert into ProcessTaskTypes (Id ,ProcessTaskTypeName,CreatedDate) values(3,'Approval', '" + DateTime.Now + "')");
            Sql("insert into ProcessTaskTypes (Id ,ProcessTaskTypeName,CreatedDate) values(4,'Form', '" + DateTime.Now + "')");
            Sql("SET IDENTITY_INSERT ProcessTaskTypes OFF");
        }
        
        public override void Down()
        {
        }
    }
}
