namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class explicitjoiningtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderBurgers", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderBurgers", "Burger_Id", "dbo.Burgers");
            DropIndex("dbo.OrderBurgers", new[] { "Order_Id" });
            DropIndex("dbo.OrderBurgers", new[] { "Burger_Id" });
            CreateTable(
                "dbo.BurgerxOrders",
                c => new
                    {
                        BurgerxOrderId = c.Int(nullable: false, identity: true),
                        BurgerId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BurgerxOrderId)
                .ForeignKey("dbo.Burgers", t => t.BurgerId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.BurgerId)
                .Index(t => t.OrderId);
            
            DropColumn("dbo.Orders", "Quantity");
            DropTable("dbo.OrderBurgers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderBurgers",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Burger_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Burger_Id });
            
            AddColumn("dbo.Orders", "Quantity", c => c.Int(nullable: false));
            DropForeignKey("dbo.BurgerxOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.BurgerxOrders", "BurgerId", "dbo.Burgers");
            DropIndex("dbo.BurgerxOrders", new[] { "OrderId" });
            DropIndex("dbo.BurgerxOrders", new[] { "BurgerId" });
            DropTable("dbo.BurgerxOrders");
            CreateIndex("dbo.OrderBurgers", "Burger_Id");
            CreateIndex("dbo.OrderBurgers", "Order_Id");
            AddForeignKey("dbo.OrderBurgers", "Burger_Id", "dbo.Burgers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderBurgers", "Order_Id", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
