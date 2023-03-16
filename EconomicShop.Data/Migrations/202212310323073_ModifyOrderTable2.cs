namespace EconomicShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyOrderTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderId", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderId");
        }
    }
}
