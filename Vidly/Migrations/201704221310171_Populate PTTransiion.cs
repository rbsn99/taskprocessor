namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatePTTransiion : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT ProcessTaskTransitions ON");
            Sql("insert into ProcessTaskTransitions (ProcessTaskTransitionId , ProcessTaskTransitionGuid, SourceTaskGuid, DestinationTaskGuid, TransitionType, CreatedDate, DeletedDate ) values('1','"+ Guid.NewGuid() + "','7fc017c8-cc83-4435-b4b6-7b26b1abcf1f','44ee2e57-09ac-48e4-aec4-d066c1952aad','Initiating','" + DateTime.Now + "', '" + DateTime.Now + "')");
            Sql("SET IDENTITY_INSERT ProcessTaskTransitions OFF");
        }
        
        public override void Down()
        {
        }
    }
}
