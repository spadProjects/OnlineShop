namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductComment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductComments", "Rate", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductComments", "Rate", c => c.Int(nullable: false));
        }
    }
}
