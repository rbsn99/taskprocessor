namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateprocesstaskrelatedclasses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.Customers", new[] { "MembershipTypeId" });
            CreateTable(
                "dbo.ProcessTaskAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessTaskAttributeGuid = c.Guid(nullable: false),
                        ProcessTaskGuid = c.Guid(nullable: false),
                        AttributeKey = c.String(),
                        AttributeValue = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProcessTaskRecipients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessTaskRecipientGuid = c.Guid(nullable: false),
                        ProcessTaskGuid = c.Guid(nullable: false),
                        RecipientId = c.Int(nullable: false),
                        RecipientType = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProcessTaskTransitions",
                c => new
                    {
                        ProcessTaskTransitionId = c.Int(nullable: false, identity: true),
                        ProcessTaskTransitionGuid = c.Guid(nullable: false),
                        SourceTaskGuid = c.Guid(nullable: false),
                        DestinationTaskGuid = c.Guid(nullable: false),
                        SourceTaskState = c.String(),
                        TransitionType = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProcessTaskTransitionId);
            
            AddColumn("dbo.ProcessTaskTypes", "AttributeKeyName", c => c.String());
            DropTable("dbo.Customers");
            DropTable("dbo.MembershipTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        SignUpFee = c.Short(nullable: false),
                        DurationInMonths = c.Byte(nullable: false),
                        DiscountRate = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsSubscribedToNewsletter = c.Boolean(nullable: false),
                        MembershipTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.ProcessTaskTypes", "AttributeKeyName");
            DropTable("dbo.ProcessTaskTransitions");
            DropTable("dbo.ProcessTaskRecipients");
            DropTable("dbo.ProcessTaskAttributes");
            CreateIndex("dbo.Customers", "MembershipTypeId");
            AddForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes", "Id", cascadeDelete: true);
        }
    }
}
