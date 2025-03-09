namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_Workday : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workday",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(nullable: false, maxLength: 50),
                        Registration = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 100),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        Notification = c.Boolean(nullable: false),
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
            DropTable("dbo.Workday");
        }
    }
}
