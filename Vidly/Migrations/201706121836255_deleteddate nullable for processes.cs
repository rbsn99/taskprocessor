namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteddatenullableforprocesses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Processes", "DeletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Processes", "DeletedDate", c => c.DateTime(nullable: false));
        }
    }
}
