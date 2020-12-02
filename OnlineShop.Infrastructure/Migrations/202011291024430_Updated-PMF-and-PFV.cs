namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPMFandPFV : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductFeatureValues", "Value", c => c.String(maxLength: 600));
            AlterColumn("dbo.ProductMainFeatures", "Value", c => c.String(maxLength: 600));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductMainFeatures", "Value", c => c.String(nullable: false, maxLength: 600));
            AlterColumn("dbo.ProductFeatureValues", "Value", c => c.String(nullable: false, maxLength: 600));
        }
    }
}
