namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restore : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Locations");
            DropPrimaryKey("dbo.Locations");
            AddColumn("dbo.Locations", "Store_Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Locations", "Store_Id");
            AddForeignKey("dbo.Orders", "StoreId", "dbo.Locations", "Store_Id", cascadeDelete: true);
            DropColumn("dbo.Locations", "StoreId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "StoreId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Locations");
            DropPrimaryKey("dbo.Locations");
            DropColumn("dbo.Locations", "Store_Id");
            AddPrimaryKey("dbo.Locations", "StoreId");
            AddForeignKey("dbo.Orders", "StoreId", "dbo.Locations", "StoreId", cascadeDelete: true);
        }
    }
}
