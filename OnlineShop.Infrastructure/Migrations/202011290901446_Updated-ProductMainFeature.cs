namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductMainFeature : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductMainFeatures", "SubFeatureId", "dbo.SubFeatures");
            DropIndex("dbo.ProductMainFeatures", new[] { "SubFeatureId" });
            AlterColumn("dbo.ProductMainFeatures", "SubFeatureId", c => c.Int());
            CreateIndex("dbo.ProductMainFeatures", "SubFeatureId");
            AddForeignKey("dbo.ProductMainFeatures", "SubFeatureId", "dbo.SubFeatures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductMainFeatures", "SubFeatureId", "dbo.SubFeatures");
            DropIndex("dbo.ProductMainFeatures", new[] { "SubFeatureId" });
            AlterColumn("dbo.ProductMainFeatures", "SubFeatureId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductMainFeatures", "SubFeatureId");
            AddForeignKey("dbo.ProductMainFeatures", "SubFeatureId", "dbo.SubFeatures", "Id", cascadeDelete: true);
        }
    }
}
