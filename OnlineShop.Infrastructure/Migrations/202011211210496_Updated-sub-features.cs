namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedsubfeatures : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SubFeatures", name: "Feature_Id", newName: "FeatureId");
            RenameIndex(table: "dbo.SubFeatures", name: "IX_Feature_Id", newName: "IX_FeatureId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SubFeatures", name: "IX_FeatureId", newName: "IX_Feature_Id");
            RenameColumn(table: "dbo.SubFeatures", name: "FeatureId", newName: "Feature_Id");
        }
    }
}
