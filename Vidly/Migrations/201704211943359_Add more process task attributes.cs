namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addmoreprocesstaskattributes : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT ProcessTaskAttributes ON");
            Sql("insert into ProcessTaskAttributes (Id , ProcessTaskAttributeGuid, ProcessTaskGuid, AttributeKey, AttributeValue, CreatedDate, DeletedDate ) values(2,'" + Guid.NewGuid() + "','44ee2e57-09ac-48e4-aec4-d066c1952aad', 'email address list', 'ryba9287@gmail.com', '" + DateTime.Now + "', '" + DateTime.Now + "')");
            Sql("insert into ProcessTaskAttributes (Id , ProcessTaskAttributeGuid, ProcessTaskGuid, AttributeKey, AttributeValue, CreatedDate, DeletedDate ) values(3,'" + Guid.NewGuid() + "','44ee2e57-09ac-48e4-aec4-d066c1952aad', 'email title', 'this is the email title', '" + DateTime.Now + "', '" + DateTime.Now + "')");
            Sql("insert into ProcessTaskAttributes (Id , ProcessTaskAttributeGuid, ProcessTaskGuid, AttributeKey, AttributeValue, CreatedDate, DeletedDate ) values(4,'" + Guid.NewGuid() + "','44ee2e57-09ac-48e4-aec4-d066c1952aad', 'email body', 'body body body', '" + DateTime.Now + "', '" + DateTime.Now + "')");
            Sql("SET IDENTITY_INSERT ProcessTaskAttributes OFF");
        }
        
        public override void Down()
        {
        }
    }
}
