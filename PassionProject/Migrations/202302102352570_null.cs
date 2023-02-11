namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _null : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Date", c => c.DateTime(nullable: false));
        }
    }
}
