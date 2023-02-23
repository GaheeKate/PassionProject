namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderBurgers", "Quantity", c => c.Int(nullable: false));

    }

    public override void Down()
        {
        }
    }
}
