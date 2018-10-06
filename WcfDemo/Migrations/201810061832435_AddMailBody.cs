namespace WcfDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMailBody : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageRequests", "MailBody", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageRequests", "MailBody");
        }
    }
}
