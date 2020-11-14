namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShopRelatedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.ProductGroupBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logo = c.String(),
                        Name = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductGroupFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductGroupId = c.String(),
                        FeatureId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        ProductGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroup_Id)
                .Index(t => t.FeatureId)
                .Index(t => t.ProductGroup_Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductMainFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SubFeatureId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Long(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.SubFeatures", t => t.SubFeatureId, cascadeDelete: true)
                .Index(t => t.FeatureId)
                .Index(t => t.ProductId)
                .Index(t => t.SubFeatureId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(maxLength: 2000),
                        Description = c.String(),
                        Image = c.String(),
                        Rate = c.Int(nullable: false),
                        ProductGroupId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId)
                .Index(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.ProductComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Email = c.String(nullable: false, maxLength: 400),
                        Message = c.String(nullable: false, maxLength: 800),
                        AddedDate = c.DateTime(),
                        Rate = c.Int(nullable: false),
                        ParentId = c.Int(),
                        ProductId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleComments", t => t.ParentId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductGalleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(nullable: false),
                        ProductId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.SubFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 600),
                        Info = c.String(maxLength: 600),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        Feature_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.Feature_Id)
                .Index(t => t.Feature_Id);
            
            AddColumn("dbo.ArticleComments", "ProductComment_Id", c => c.Int());
            CreateIndex("dbo.ArticleComments", "ProductComment_Id");
            AddForeignKey("dbo.ArticleComments", "ProductComment_Id", "dbo.ProductComments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductGroupFeatures", "ProductGroup_Id", "dbo.ProductGroups");
            DropForeignKey("dbo.SubFeatures", "Feature_Id", "dbo.Features");
            DropForeignKey("dbo.ProductMainFeatures", "SubFeatureId", "dbo.SubFeatures");
            DropForeignKey("dbo.ProductMainFeatures", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGalleries", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductComments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductComments", "ParentId", "dbo.ArticleComments");
            DropForeignKey("dbo.ArticleComments", "ProductComment_Id", "dbo.ProductComments");
            DropForeignKey("dbo.ProductMainFeatures", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.ProductGroupFeatures", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.ProductGroupBrands", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroupBrands", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.ProductGroups", "ParentId", "dbo.ProductGroups");
            DropIndex("dbo.SubFeatures", new[] { "Feature_Id" });
            DropIndex("dbo.ProductGalleries", new[] { "ProductId" });
            DropIndex("dbo.ProductComments", new[] { "ProductId" });
            DropIndex("dbo.ProductComments", new[] { "ParentId" });
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            DropIndex("dbo.ProductMainFeatures", new[] { "SubFeatureId" });
            DropIndex("dbo.ProductMainFeatures", new[] { "ProductId" });
            DropIndex("dbo.ProductMainFeatures", new[] { "FeatureId" });
            DropIndex("dbo.ProductGroupFeatures", new[] { "ProductGroup_Id" });
            DropIndex("dbo.ProductGroupFeatures", new[] { "FeatureId" });
            DropIndex("dbo.ProductGroupBrands", new[] { "BrandId" });
            DropIndex("dbo.ProductGroupBrands", new[] { "ProductGroupId" });
            DropIndex("dbo.ProductGroups", new[] { "ParentId" });
            DropIndex("dbo.ArticleComments", new[] { "ProductComment_Id" });
            DropColumn("dbo.ArticleComments", "ProductComment_Id");
            DropTable("dbo.SubFeatures");
            DropTable("dbo.ProductGalleries");
            DropTable("dbo.ProductComments");
            DropTable("dbo.Products");
            DropTable("dbo.ProductMainFeatures");
            DropTable("dbo.Features");
            DropTable("dbo.ProductGroupFeatures");
            DropTable("dbo.Brands");
            DropTable("dbo.ProductGroupBrands");
            DropTable("dbo.ProductGroups");
        }
    }
}
