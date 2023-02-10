namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifiedtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "BurgerId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Quantity", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "BurgerId");
            AddForeignKey("dbo.Orders", "BurgerId", "dbo.Burgers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "BurgerId", "dbo.Burgers");
            DropIndex("dbo.Orders", new[] { "BurgerId" });
            DropColumn("dbo.Orders", "Quantity");
            DropColumn("dbo.Orders", "BurgerId");
        }
    }
}
