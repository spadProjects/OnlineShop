namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductFeatureValueAndProductMainFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductFeatureValues", "Value", c => c.String(nullable: false, maxLength: 600));
            AddColumn("dbo.ProductMainFeatures", "Value", c => c.String(nullable: false, maxLength: 600));
            AddColumn("dbo.ProductMainFeatures", "OtherInfo", c => c.String(maxLength: 600));
            DropColumn("dbo.ProductFeatureValues", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductFeatureValues", "Title", c => c.String(nullable: false, maxLength: 600));
            DropColumn("dbo.ProductMainFeatures", "OtherInfo");
            DropColumn("dbo.ProductMainFeatures", "Value");
            DropColumn("dbo.ProductFeatureValues", "Value");
        }
    }
}
