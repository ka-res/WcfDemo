namespace WcfDemo.Database.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactTypeId = c.Int(nullable: false),
                        Value = c.String(),
                        MessageRequestId = c.Int(nullable: false),
                        SaveDate = c.DateTime(nullable: false),
                        IsSoftDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactTypes", t => t.ContactTypeId, cascadeDelete: true)
                .ForeignKey("dbo.MessageRequests", t => t.MessageRequestId, cascadeDelete: true)
                .Index(t => t.ContactTypeId)
                .Index(t => t.MessageRequestId);
            
            CreateTable(
                "dbo.ContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Description = c.String(),
                        SaveDate = c.DateTime(nullable: false),
                        IsSoftDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        LegalFormId = c.Int(nullable: false),
                        MessageResponseId = c.Int(nullable: false),
                        SaveDate = c.DateTime(nullable: false),
                        IsSoftDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LegalForms", t => t.LegalFormId, cascadeDelete: true)
                .ForeignKey("dbo.MessageResponses", t => t.MessageResponseId, cascadeDelete: true)
                .Index(t => t.LegalFormId)
                .Index(t => t.MessageResponseId);
            
            CreateTable(
                "dbo.LegalForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Description = c.String(),
                        SaveDate = c.DateTime(nullable: false),
                        IsSoftDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReturnCodeId = c.Int(nullable: false),
                        ErrorMessage = c.String(),
                        SaveDate = c.DateTime(nullable: false),
                        IsSoftDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReturnCodes", t => t.ReturnCodeId, cascadeDelete: true)
                .Index(t => t.ReturnCodeId);
            
            CreateTable(
                "dbo.ReturnCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Description = c.String(),
                        SaveDate = c.DateTime(nullable: false),
                        IsSoftDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageRequests", "MessageResponseId", "dbo.MessageResponses");
            DropForeignKey("dbo.MessageResponses", "ReturnCodeId", "dbo.ReturnCodes");
            DropForeignKey("dbo.MessageRequests", "LegalFormId", "dbo.LegalForms");
            DropForeignKey("dbo.Contacts", "MessageRequestId", "dbo.MessageRequests");
            DropForeignKey("dbo.Contacts", "ContactTypeId", "dbo.ContactTypes");
            DropIndex("dbo.MessageResponses", new[] { "ReturnCodeId" });
            DropIndex("dbo.MessageRequests", new[] { "MessageResponseId" });
            DropIndex("dbo.MessageRequests", new[] { "LegalFormId" });
            DropIndex("dbo.Contacts", new[] { "MessageRequestId" });
            DropIndex("dbo.Contacts", new[] { "ContactTypeId" });
            DropTable("dbo.ReturnCodes");
            DropTable("dbo.MessageResponses");
            DropTable("dbo.LegalForms");
            DropTable("dbo.MessageRequests");
            DropTable("dbo.ContactTypes");
            DropTable("dbo.Contacts");
        }
    }
}
