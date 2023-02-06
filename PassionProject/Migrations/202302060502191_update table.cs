namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Locations");
            DropPrimaryKey("dbo.Locations");
            AddColumn("dbo.Locations", "StoreId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Locations", "StoreId");
            AddForeignKey("dbo.Orders", "StoreId", "dbo.Locations", "StoreId", cascadeDelete: true);
            DropColumn("dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Locations");
            DropPrimaryKey("dbo.Locations");
            DropColumn("dbo.Locations", "StoreId");
            AddPrimaryKey("dbo.Locations", "Id");
            AddForeignKey("dbo.Orders", "StoreId", "dbo.Locations", "Id", cascadeDelete: true);
        }
    }
}
