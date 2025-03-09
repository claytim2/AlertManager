namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_Jos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jos", "EmployeeID", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Jos", "Registration", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Jos", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Jos", "UserName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Jos", "Email", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.Jos", "Notification", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jos", "Notification");
            DropColumn("dbo.Jos", "Email");
            DropColumn("dbo.Jos", "UserName");
            DropColumn("dbo.Jos", "Name");
            DropColumn("dbo.Jos", "Registration");
            DropColumn("dbo.Jos", "EmployeeID");
        }
    }
}
