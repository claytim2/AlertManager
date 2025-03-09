namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_Rh_Lpa_Jos_Gap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteId = c.Int(nullable: false),
                        UserCreate = c.String(),
                        DateCreate = c.DateTime(),
                        UserUpdate = c.String(),
                        DateUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LpaBel",
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
            
            CreateTable(
                "dbo.LpaVls",
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
            
            CreateTable(
                "dbo.Rh",
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
            DropTable("dbo.Rh");
            DropTable("dbo.LpaVls");
            DropTable("dbo.LpaBel");
            DropTable("dbo.Jos");
        }
    }
}
