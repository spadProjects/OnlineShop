namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeaddressnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Customers", "NationalCode", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "NationalCode", c => c.String(maxLength: 600));
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false, maxLength: 600));
        }
    }
}
