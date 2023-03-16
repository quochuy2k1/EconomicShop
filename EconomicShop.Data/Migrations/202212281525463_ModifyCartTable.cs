namespace EconomicShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCartTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "AppUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Carts", new[] { "AppUser_Id" });
            DropColumn("dbo.Carts", "UserId");
            RenameColumn(table: "dbo.Carts", name: "AppUser_Id", newName: "UserId");
            AddColumn("dbo.Orders", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "OrderDetailId", c => c.Int(nullable: false));
            AlterColumn("dbo.Carts", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Carts", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Carts", "UserId");
            AddForeignKey("dbo.Carts", "UserId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Carts", "UserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Carts", new[] { "UserId" });
            AlterColumn("dbo.Carts", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Carts", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.Orders", "OrderDetailId");
            DropColumn("dbo.Orders", "UserId");
            RenameColumn(table: "dbo.Carts", name: "UserId", newName: "AppUser_Id");
            AddColumn("dbo.Carts", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Carts", "AppUser_Id");
            AddForeignKey("dbo.Carts", "AppUser_Id", "dbo.ApplicationUsers", "Id");
        }
    }
}
