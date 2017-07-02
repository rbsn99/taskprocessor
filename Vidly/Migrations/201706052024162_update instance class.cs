namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinstanceclass : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Instances", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Instances", "MyProperty", c => c.Int(nullable: false));
        }
    }
}
