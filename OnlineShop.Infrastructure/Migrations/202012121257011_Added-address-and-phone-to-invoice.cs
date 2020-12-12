namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedaddressandphonetoinvoice : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "User_Id" });
            DropColumn("dbo.Customers", "UserId");
            RenameColumn(table: "dbo.Customers", name: "User_Id", newName: "UserId");
            AddColumn("dbo.Invoices", "Address", c => c.String(maxLength: 500));
            AddColumn("dbo.Invoices", "Phone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "UserId" });
            AlterColumn("dbo.Customers", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Invoices", "Phone");
            DropColumn("dbo.Invoices", "Address");
            RenameColumn(table: "dbo.Customers", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Customers", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "User_Id");
        }
    }
}
