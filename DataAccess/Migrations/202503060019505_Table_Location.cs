namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_Location : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Area = c.String(nullable: false, maxLength: 50),
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
            DropTable("dbo.Location");
        }
    }
}
