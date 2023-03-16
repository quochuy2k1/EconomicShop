namespace EconomicShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyOrderTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "OrderDetailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderDetailId", c => c.Int(nullable: false));
        }
    }
}
