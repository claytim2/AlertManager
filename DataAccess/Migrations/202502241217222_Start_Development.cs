namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start_Development : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutConfiguration",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contact1Name = c.String(),
                        Contact1Email = c.String(),
                        Contact1Phone = c.String(),
                        Contact2Name = c.String(),
                        Contact2Email = c.String(),
                        Contact2Phone = c.String(),
                        Contact3Name = c.String(),
                        Contact3Email = c.String(),
                        Contact3Phone = c.String(),
                        Contact4Name = c.String(),
                        Contact4Email = c.String(),
                        Contact4Phone = c.String(),
                        SiteId = c.Int(nullable: false),
                        UserCreate = c.String(),
                        DateCreate = c.DateTime(),
                        UserUpdate = c.String(),
                        DateUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        IpAddress = c.String(),
                        LogType = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Description = c.String(),
                        UserName = c.String(),
                        SiteId = c.Int(nullable: false),
                        UserCreate = c.String(),
                        DateCreate = c.DateTime(),
                        UserUpdate = c.String(),
                        DateUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DateTime);
            
            CreateTable(
                "dbo.SystemConfiguration",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(nullable: false),
                        SiteId = c.Int(nullable: false),
                        UserCreate = c.String(),
                        DateCreate = c.DateTime(),
                        UserUpdate = c.String(),
                        DateUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        Roles = c.String(nullable: false, maxLength: 50),
                        Registration = c.String(nullable: false, maxLength: 50),
                        Active = c.Boolean(nullable: false),
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
            DropIndex("dbo.Log", new[] { "DateTime" });
            DropTable("dbo.Users");
            DropTable("dbo.SystemConfiguration");
            DropTable("dbo.Log");
            DropTable("dbo.AboutConfiguration");
        }
    }
}
