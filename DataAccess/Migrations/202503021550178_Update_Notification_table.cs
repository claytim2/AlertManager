namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Notification_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notification", "Active");
        }
    }
}
