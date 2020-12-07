namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateddiscounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discounts", "GroupIdentifier", c => c.String());
            AddColumn("dbo.Discounts", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discounts", "Title");
            DropColumn("dbo.Discounts", "GroupIdentifier");
        }
    }
}
