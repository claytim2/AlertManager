namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_table_location : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Location", "Description", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Location", "Description");
        }
    }
}
