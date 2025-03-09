namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notification_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Area = c.String(nullable: false, maxLength: 50),
                        Message = c.String(nullable: false, maxLength: 300),
                        SiteId = c.Int(nullable: false),
                        UserCreate = c.String(),
                        DateCreate = c.DateTime(),
                        UserUpdate = c.String(),
                        DateUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notification");
        }
    }
}
