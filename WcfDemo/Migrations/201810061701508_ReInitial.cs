namespace WcfDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReInitial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MessageRequests", "FirstName", c => c.String());
            AlterColumn("dbo.MessageRequests", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MessageRequests", "LastName", c => c.String());
            AlterColumn("dbo.MessageRequests", "FirstName", c => c.String(nullable: false));
        }
    }
}
