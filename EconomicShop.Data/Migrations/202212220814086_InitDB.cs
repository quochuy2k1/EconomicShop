﻿namespace EconomicShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationGroups",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 250),
                    Description = c.String(maxLength: 250),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                {
                    GroupId = c.Int(nullable: false),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.GroupId, t.RoleId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.ApplicationRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                    Description = c.String(maxLength: 250),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ApplicationUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                    ApplicationUser_Id = c.String(maxLength: 128),
                    IdentityRole_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.ApplicationRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);

            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    GroupId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);

            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FullName = c.String(maxLength: 256),
                    Address = c.String(maxLength: 256),
                    BirthDay = c.DateTime(),
                    Email = c.String(),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ApplicationUserClaims",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    Id = c.Int(nullable: false),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                    ApplicationUser_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);

            CreateTable(
                "dbo.ApplicationUserLogins",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    LoginProvider = c.String(),
                    ProviderKey = c.String(),
                    ApplicationUser_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);

            CreateTable(
                "dbo.Errors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Message = c.String(),
                    StackTrace = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Footers",
                c => new
                {
                    ID = c.String(nullable: false, maxLength: 128),
                    Content = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.MenuGroups",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Menus",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    URL = c.String(nullable: false),
                    DisplayOrder = c.Int(),
                    GroupId = c.Int(nullable: false),
                    Target = c.String(),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuGroups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);

            CreateTable(
                "dbo.OrderDetails",
                c => new
                {
                    OrderID = c.Int(nullable: false),
                    ProductID = c.Int(nullable: false),
                    Quantity = c.Int(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => new { t.OrderID, t.ProductID })
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);

            CreateTable(
                "dbo.Orders",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    CustomerName = c.String(nullable: false, maxLength: 256),
                    CustomerAddress = c.String(nullable: false, maxLength: 256),
                    CustomerEmail = c.String(nullable: false, maxLength: 256),
                    CustomerMobile = c.String(nullable: false, maxLength: 50),
                    CustomerMessage = c.String(nullable: false, maxLength: 256),
                    PaymentMethod = c.String(maxLength: 256),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    PaymentStatus = c.String(),
                    Status = c.Boolean(nullable: false),
                    CustomerId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Products",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    CategoryId = c.Int(nullable: false),
                    Image = c.String(nullable: false, maxLength: 255),
                    MoreImages = c.String(storeType: "xml"),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Promotion = c.Decimal(precision: 18, scale: 2),
                    Warranty = c.Int(),
                    Description = c.String(nullable: false, maxLength: 512),
                    Content = c.String(),
                    HomeFlag = c.Boolean(),
                    HotFlag = c.Boolean(),
                    ViewCount = c.Int(),
                    Tags = c.String(),
                    Quantity = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 255),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 255),
                    Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                    MetaKeyword = c.String(maxLength: 255),
                    MetaDescription = c.String(maxLength: 255),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);

            CreateTable(
                "dbo.ProductCategories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    Description = c.String(nullable: false, maxLength: 512),
                    ParentId = c.Int(),
                    DisplayOrder = c.Int(),
                    Image = c.String(nullable: false, maxLength: 255),
                    HomeFlag = c.Boolean(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 255),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 255),
                    Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                    MetaKeyword = c.String(maxLength: 255),
                    MetaDescription = c.String(maxLength: 255),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Pages",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Content = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 255),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 255),
                    Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                    MetaKeyword = c.String(maxLength: 255),
                    MetaDescription = c.String(maxLength: 255),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.PostCategories",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Description = c.String(maxLength: 500),
                    ParentID = c.Int(),
                    DisplayOrder = c.Int(),
                    Image = c.String(maxLength: 256),
                    HomeFlag = c.Boolean(),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 255),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 255),
                    Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                    MetaKeyword = c.String(maxLength: 255),
                    MetaDescription = c.String(maxLength: 255),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Posts",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    CategoryID = c.Int(nullable: false),
                    Image = c.String(maxLength: 256),
                    Description = c.String(maxLength: 500),
                    Content = c.String(),
                    HomeFlag = c.Boolean(),
                    HotFlag = c.Boolean(),
                    ViewCount = c.Int(),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 255),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 255),
                    Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                    MetaKeyword = c.String(maxLength: 255),
                    MetaDescription = c.String(maxLength: 255),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PostCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);

            CreateTable(
                "dbo.PostTags",
                c => new
                {
                    PostID = c.Int(nullable: false),
                    TagID = c.String(nullable: false, maxLength: 50, unicode: false),
                })
                .PrimaryKey(t => new { t.PostID, t.TagID })
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.TagID);

            CreateTable(
                "dbo.Tags",
                c => new
                {
                    ID = c.String(nullable: false, maxLength: 50, unicode: false),
                    Name = c.String(nullable: false, maxLength: 50),
                    Type = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.ProductTags",
                c => new
                {
                    ProductID = c.Int(nullable: false),
                    TagID = c.String(nullable: false, maxLength: 50, unicode: false),
                })
                .PrimaryKey(t => new { t.ProductID, t.TagID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.TagID);

            CreateTable(
                "dbo.Slides",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Description = c.String(maxLength: 256),
                    Image = c.String(maxLength: 256),
                    Url = c.String(maxLength: 256),
                    DisplayOrder = c.Int(),
                    Status = c.Boolean(nullable: false),
                    Content = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.SupportOnlines",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Department = c.String(maxLength: 50),
                    Skype = c.String(maxLength: 50),
                    Mobile = c.String(maxLength: 50),
                    Email = c.String(maxLength: 50),
                    Yahoo = c.String(maxLength: 50),
                    Facebook = c.String(maxLength: 50),
                    Status = c.Boolean(nullable: false),
                    DisplayOrder = c.Int(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.SystemConfigs",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 50, unicode: false),
                    ValueString = c.String(maxLength: 50),
                    ValueInt = c.Int(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.VisitorStatistics",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    VisitedDate = c.DateTime(nullable: false),
                    IPAddress = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ProductTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products");
            DropForeignKey("dbo.PostTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.PostTags", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Menus", "GroupId", "dbo.MenuGroups");
            DropForeignKey("dbo.ApplicationUserGroups", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.ProductTags", new[] { "TagID" });
            DropIndex("dbo.ProductTags", new[] { "ProductID" });
            DropIndex("dbo.PostTags", new[] { "TagID" });
            DropIndex("dbo.PostTags", new[] { "PostID" });
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Menus", new[] { "GroupId" });
            DropIndex("dbo.ApplicationUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "UserId" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropTable("dbo.VisitorStatistics");
            DropTable("dbo.SystemConfigs");
            DropTable("dbo.SupportOnlines");
            DropTable("dbo.Slides");
            DropTable("dbo.ProductTags");
            DropTable("dbo.Tags");
            DropTable("dbo.PostTags");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Pages");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Menus");
            DropTable("dbo.MenuGroups");
            DropTable("dbo.Footers");
            DropTable("dbo.Errors");
            DropTable("dbo.ApplicationUserLogins");
            DropTable("dbo.ApplicationUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationUserRoles");
            DropTable("dbo.ApplicationRoles");
            DropTable("dbo.ApplicationRoleGroups");
            DropTable("dbo.ApplicationGroups");
        }
    }
}