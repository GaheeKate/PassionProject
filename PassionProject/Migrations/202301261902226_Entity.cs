namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreName = c.String(),
                        StoreLocation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        BurgerId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Burgers", "Cheese");
            DropColumn("dbo.Burgers", "Bun");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Burgers", "Bun", c => c.Int(nullable: false));
            AddColumn("dbo.Burgers", "Cheese", c => c.Int(nullable: false));
            DropTable("dbo.Orders");
            DropTable("dbo.Locations");
        }
    }
}
