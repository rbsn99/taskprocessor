namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReferenceToIdentityModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessGuid = c.Guid(nullable: false),
                        ProcessName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProcessTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessGuid = c.Guid(nullable: false),
                        TaskName = c.String(),
                        TaskType = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProcessTasks");
            DropTable("dbo.Processes");
        }
    }
}
