namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table_update : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.BurgerxOrderId)
                .ForeignKey("dbo.Burgers", t => t.BurgerId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.BurgerId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BurgerxOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.BurgerxOrders", "BurgerId", "dbo.Burgers");
            DropIndex("dbo.BurgerxOrders", new[] { "OrderId" });
            DropIndex("dbo.BurgerxOrders", new[] { "BurgerId" });
            DropTable("dbo.BurgerxOrders");
        }
    }
}
