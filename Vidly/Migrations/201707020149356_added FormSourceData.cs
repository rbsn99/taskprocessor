namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFormSourceData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormSourceDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormSourceDataGuid = c.Guid(nullable: false),
                        FormName = c.String(),
                        FormDescription = c.String(),
                        FormSourceCode = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FormSourceDatas");
        }
    }
}
