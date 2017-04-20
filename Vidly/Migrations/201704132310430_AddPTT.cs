namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPTT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcessTaskTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessTaskTypeName = c.String(),
                        CreatedDate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProcessTaskTypes");
        }
    }
}
