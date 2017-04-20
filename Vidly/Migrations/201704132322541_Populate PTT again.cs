namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatePTTagain : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT ProcessTaskTypes ON");
            Sql("insert into ProcessTaskTypes (Id ,ProcessTaskTypeName,CreatedDate) values(1,'Milestone', '" + DateTime.Now + "')");
            Sql("insert into ProcessTaskTypes (Id ,ProcessTaskTypeName,CreatedDate) values(2,'E-mail notification', '" + DateTime.Now + "')");
            Sql("SET IDENTITY_INSERT ProcessTaskTypes OFF");
        }
        
        public override void Down()
        {
        }
    }
}
