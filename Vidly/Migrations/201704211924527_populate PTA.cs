namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populatePTA : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT ProcessTaskAttributes ON");
            Sql("insert into ProcessTaskAttributes (Id , ProcessTaskAttributeGuid, ProcessTaskGuid, AttributeKey, AttributeValue, CreatedDate, DeletedDate ) values(1,'" + Guid.NewGuid() + "','47f246c9-d36d-43ae-a6cd-a16bcf9977a2', 'milestone text', 'Submitted', '"+DateTime.Now+ "', '" + DateTime.Now + "')");
            Sql("SET IDENTITY_INSERT ProcessTaskAttributes OFF");
        }
        
        public override void Down()
        {
        }
    }
}
