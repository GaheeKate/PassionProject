namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class re : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderBurgers1", newName: "OrderBurgers");
            DropTable("dbo.OrderBurgers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderBurgers",
                c => new
                    {
                        Order_id = c.Int(nullable: false, identity: true),
                        Burger_id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Order_id);
            
            RenameTable(name: "dbo.OrderBurgers", newName: "OrderBurgers1");
        }
    }
}
