namespace EconomicShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyOrderTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "CustomerMessage", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "CustomerMessage", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.Orders", "Total");
        }
    }
}
