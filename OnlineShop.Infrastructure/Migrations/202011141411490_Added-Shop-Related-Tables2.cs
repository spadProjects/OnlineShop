namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShopRelatedTables2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscountType = c.Int(nullable: false),
                        Amount = c.Long(nullable: false),
                        OfferId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductGroupId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Offers", t => t.OfferId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.OfferId)
                .Index(t => t.ProductId)
                .Index(t => t.ProductGroupId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        Image = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductFeatureValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        OtherInfo = c.String(maxLength: 600),
                        FeatureId = c.Int(),
                        ProductId = c.Int(),
                        SubFeatureId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId)
                .ForeignKey("dbo.SubFeatures", t => t.SubFeatureId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.FeatureId)
                .Index(t => t.ProductId)
                .Index(t => t.SubFeatureId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(nullable: false, maxLength: 600),
                        NationalCode = c.String(maxLength: 600),
                        Address = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalPrice = c.Long(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        InvoiceNumber = c.String(),
                        CustomerId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Long(nullable: false),
                        Price = c.Long(nullable: false),
                        InvoiceId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .Index(t => t.InvoiceId);
            
            AddColumn("dbo.SubFeatures", "OtherInfo", c => c.String(maxLength: 600));
            DropColumn("dbo.SubFeatures", "Info");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubFeatures", "Info", c => c.String(maxLength: 600));
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ProductFeatureValues", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductFeatureValues", "SubFeatureId", "dbo.SubFeatures");
            DropForeignKey("dbo.Discounts", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductFeatureValues", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.Discounts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Discounts", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.Discounts", "BrandId", "dbo.Brands");
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.ProductFeatureValues", new[] { "SubFeatureId" });
            DropIndex("dbo.ProductFeatureValues", new[] { "ProductId" });
            DropIndex("dbo.ProductFeatureValues", new[] { "FeatureId" });
            DropIndex("dbo.Discounts", new[] { "BrandId" });
            DropIndex("dbo.Discounts", new[] { "ProductGroupId" });
            DropIndex("dbo.Discounts", new[] { "ProductId" });
            DropIndex("dbo.Discounts", new[] { "OfferId" });
            DropColumn("dbo.SubFeatures", "OtherInfo");
            DropTable("dbo.InvoiceItems");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
            DropTable("dbo.ProductFeatureValues");
            DropTable("dbo.Offers");
            DropTable("dbo.Discounts");
        }
    }
}
