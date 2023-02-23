namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class r : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderBurgers1", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderBurgers1", "Burger_Id", "dbo.Burgers");
            DropIndex("dbo.OrderBurgers1", new[] { "Order_Id" });
            DropIndex("dbo.OrderBurgers1", new[] { "Burger_Id" });

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderBurgers1",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Burger_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Burger_Id });
            
            CreateTable(
                "dbo.OrderBurgers",
                c => new
                    {
                        Order_id = c.Int(nullable: false, identity: true),
                        Burger_id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Order_id);
            
            CreateIndex("dbo.OrderBurgers1", "Burger_Id");
            CreateIndex("dbo.OrderBurgers1", "Order_Id");
            AddForeignKey("dbo.OrderBurgers1", "Burger_Id", "dbo.Burgers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderBurgers1", "Order_Id", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
