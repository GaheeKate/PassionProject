namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mmt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "BurgerId", "dbo.Burgers");
            DropIndex("dbo.Orders", new[] { "BurgerId" });
            CreateTable(
                "dbo.OrderBurgers",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Burger_Id = c.Int(nullable: false),
                  
                })
                .PrimaryKey(t => new { t.Order_Id, t.Burger_Id })
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Burgers", t => t.Burger_Id, cascadeDelete: true)
                .Index(t => t.Order_Id)
                .Index(t => t.Burger_Id);
            
            DropColumn("dbo.Orders", "BurgerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "BurgerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderBurgers", "Burger_Id", "dbo.Burgers");
            DropForeignKey("dbo.OrderBurgers", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderBurgers", new[] { "Burger_Id" });
            DropIndex("dbo.OrderBurgers", new[] { "Order_Id" });
            DropTable("dbo.OrderBurgers");
            CreateIndex("dbo.Orders", "BurgerId");
            AddForeignKey("dbo.Orders", "BurgerId", "dbo.Burgers", "Id", cascadeDelete: true);
        }
    }
}
