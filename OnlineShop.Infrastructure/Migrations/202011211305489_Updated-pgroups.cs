namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedpgroups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductGroups", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductGroups", "Image");
        }
    }
}
