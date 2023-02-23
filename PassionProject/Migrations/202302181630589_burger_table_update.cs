namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class burger_table_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Burgers", "BurgerPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Burgers", "BurgerPrice");
        }
    }
}
