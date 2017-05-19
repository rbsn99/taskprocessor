namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedeleteddatetobenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProcessTaskTransitions", "DeletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProcessTaskTransitions", "DeletedDate", c => c.DateTime(nullable: false));
        }
    }
}
