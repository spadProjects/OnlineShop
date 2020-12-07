namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateddiscount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discounts", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Discounts", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.Discounts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Discounts", "ProductGroupId", "dbo.ProductGroups");
            DropIndex("dbo.Discounts", new[] { "OfferId" });
            DropIndex("dbo.Discounts", new[] { "ProductId" });
            DropIndex("dbo.Discounts", new[] { "ProductGroupId" });
            DropIndex("dbo.Discounts", new[] { "BrandId" });
            AlterColumn("dbo.Discounts", "OfferId", c => c.Int());
            AlterColumn("dbo.Discounts", "ProductId", c => c.Int());
            AlterColumn("dbo.Discounts", "ProductGroupId", c => c.Int());
            AlterColumn("dbo.Discounts", "BrandId", c => c.Int());
            CreateIndex("dbo.Discounts", "OfferId");
            CreateIndex("dbo.Discounts", "ProductId");
            CreateIndex("dbo.Discounts", "ProductGroupId");
            CreateIndex("dbo.Discounts", "BrandId");
            AddForeignKey("dbo.Discounts", "BrandId", "dbo.Brands", "Id");
            AddForeignKey("dbo.Discounts", "OfferId", "dbo.Offers", "Id");
            AddForeignKey("dbo.Discounts", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Discounts", "ProductGroupId", "dbo.ProductGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discounts", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.Discounts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Discounts", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.Discounts", "BrandId", "dbo.Brands");
            DropIndex("dbo.Discounts", new[] { "BrandId" });
            DropIndex("dbo.Discounts", new[] { "ProductGroupId" });
            DropIndex("dbo.Discounts", new[] { "ProductId" });
            DropIndex("dbo.Discounts", new[] { "OfferId" });
            AlterColumn("dbo.Discounts", "BrandId", c => c.Int(nullable: false));
            AlterColumn("dbo.Discounts", "ProductGroupId", c => c.Int(nullable: false));
            AlterColumn("dbo.Discounts", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.Discounts", "OfferId", c => c.Int(nullable: false));
            CreateIndex("dbo.Discounts", "BrandId");
            CreateIndex("dbo.Discounts", "ProductGroupId");
            CreateIndex("dbo.Discounts", "ProductId");
            CreateIndex("dbo.Discounts", "OfferId");
            AddForeignKey("dbo.Discounts", "ProductGroupId", "dbo.ProductGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Discounts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Discounts", "OfferId", "dbo.Offers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Discounts", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
        }
    }
}
