namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testjoining : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderBurgers", newName: "OrderBurgers1");
            CreateTable(
                "dbo.OrderBurgers",
                c => new
                    {
                        Order_id = c.Int(nullable: false, identity: true),
                        Burger_id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Order_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderBurgers");
            RenameTable(name: "dbo.OrderBurgers1", newName: "OrderBurgers");
        }
    }
}
