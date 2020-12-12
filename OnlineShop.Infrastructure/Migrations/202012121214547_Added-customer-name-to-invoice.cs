namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcustomernametoinvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "CustomerName", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "CustomerName");
        }
    }
}
