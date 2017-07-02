namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteddatenullableforprocesstaskattributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProcessTaskAttributes", "DeletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProcessTaskAttributes", "DeletedDate", c => c.DateTime(nullable: false));
        }
    }
}
