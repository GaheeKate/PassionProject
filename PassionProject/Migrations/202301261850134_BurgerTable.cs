namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BurgerTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Menus", newName: "Burgers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Burgers", newName: "Menus");
        }
    }
}
