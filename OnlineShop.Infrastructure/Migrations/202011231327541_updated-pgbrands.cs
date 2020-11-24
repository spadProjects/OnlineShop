namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedpgbrands : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductGroupFeatures", "ProductGroup_Id", "dbo.ProductGroups");
            DropIndex("dbo.ProductGroupFeatures", new[] { "ProductGroup_Id" });
            DropColumn("dbo.ProductGroupFeatures", "ProductGroupId");
            RenameColumn(table: "dbo.ProductGroupFeatures", name: "ProductGroup_Id", newName: "ProductGroupId");
            AlterColumn("dbo.ProductGroupFeatures", "ProductGroupId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductGroupFeatures", "ProductGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductGroupFeatures", "ProductGroupId");
            AddForeignKey("dbo.ProductGroupFeatures", "ProductGroupId", "dbo.ProductGroups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductGroupFeatures", "ProductGroupId", "dbo.ProductGroups");
            DropIndex("dbo.ProductGroupFeatures", new[] { "ProductGroupId" });
            AlterColumn("dbo.ProductGroupFeatures", "ProductGroupId", c => c.Int());
            AlterColumn("dbo.ProductGroupFeatures", "ProductGroupId", c => c.String());
            RenameColumn(table: "dbo.ProductGroupFeatures", name: "ProductGroupId", newName: "ProductGroup_Id");
            AddColumn("dbo.ProductGroupFeatures", "ProductGroupId", c => c.String());
            CreateIndex("dbo.ProductGroupFeatures", "ProductGroup_Id");
            AddForeignKey("dbo.ProductGroupFeatures", "ProductGroup_Id", "dbo.ProductGroups", "Id");
        }
    }
}
