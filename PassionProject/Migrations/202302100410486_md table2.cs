namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mdtable2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BurgerxOrders", "BurgerId", "dbo.Burgers");
            DropForeignKey("dbo.BurgerxOrders", "OrderId", "dbo.Orders");
            DropIndex("dbo.BurgerxOrders", new[] { "BurgerId" });
            DropIndex("dbo.BurgerxOrders", new[] { "OrderId" });
            DropTable("dbo.BurgerxOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BurgerxOrders",
                c => new
                    {
                        BurgerxOrderId = c.Int(nullable: false, identity: true),
                        BurgerId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BurgerxOrderId);
            
            CreateIndex("dbo.BurgerxOrders", "OrderId");
            CreateIndex("dbo.BurgerxOrders", "BurgerId");
            AddForeignKey("dbo.BurgerxOrders", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BurgerxOrders", "BurgerId", "dbo.Burgers", "Id", cascadeDelete: true);
        }
    }
}
