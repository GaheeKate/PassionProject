namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Quantity");
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
            AddColumn("dbo.Orders", "Quantity", c => c.Int(nullable: false));
        }
    }
}
