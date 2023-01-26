namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Foreignkey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "StoreId");
            CreateIndex("dbo.Orders", "BurgerId");
            AddForeignKey("dbo.Orders", "BurgerId", "dbo.Burgers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "StoreId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Locations");
            DropForeignKey("dbo.Orders", "BurgerId", "dbo.Burgers");
            DropIndex("dbo.Orders", new[] { "BurgerId" });
            DropIndex("dbo.Orders", new[] { "StoreId" });
        }
    }
}
