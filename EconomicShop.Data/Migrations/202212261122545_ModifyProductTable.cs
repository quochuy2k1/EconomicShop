namespace EconomicShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyProductTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Alias", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.Products", "Status", c => c.Boolean());
            AlterColumn("dbo.ProductCategories", "Alias", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.ProductCategories", "Status", c => c.Boolean());
            AlterColumn("dbo.Pages", "Alias", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.Pages", "Status", c => c.Boolean());
            AlterColumn("dbo.PostCategories", "Alias", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.PostCategories", "Status", c => c.Boolean());
            AlterColumn("dbo.Posts", "Alias", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.Posts", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Posts", "Alias", c => c.String(nullable: false, maxLength: 256, unicode: false));
            AlterColumn("dbo.PostCategories", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PostCategories", "Alias", c => c.String(nullable: false, maxLength: 256, unicode: false));
            AlterColumn("dbo.Pages", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pages", "Alias", c => c.String(nullable: false, maxLength: 256, unicode: false));
            AlterColumn("dbo.ProductCategories", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProductCategories", "Alias", c => c.String(nullable: false, maxLength: 256, unicode: false));
            AlterColumn("dbo.Products", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "Alias", c => c.String(nullable: false, maxLength: 256, unicode: false));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
