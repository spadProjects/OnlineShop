namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedgeodivision : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoDivisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 200),
                        LatinTitle = c.String(maxLength: 200),
                        GeoDivisionType = c.Int(nullable: false),
                        IsLeaf = c.Int(),
                        IsArchived = c.Int(),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeoDivisions", t => t.ParentId)
                .Index(t => t.ParentId);
            
            AddColumn("dbo.Invoices", "GeoDivisionId", c => c.Int());
            AddColumn("dbo.Customers", "PostalCode", c => c.String(maxLength: 100));
            AddColumn("dbo.Customers", "GeoDivisionId", c => c.Int());
            CreateIndex("dbo.Invoices", "GeoDivisionId");
            CreateIndex("dbo.Customers", "GeoDivisionId");
            AddForeignKey("dbo.Customers", "GeoDivisionId", "dbo.GeoDivisions", "Id");
            AddForeignKey("dbo.Invoices", "GeoDivisionId", "dbo.GeoDivisions", "Id");
            DropColumn("dbo.Customers", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Phone", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.Invoices", "GeoDivisionId", "dbo.GeoDivisions");
            DropForeignKey("dbo.Customers", "GeoDivisionId", "dbo.GeoDivisions");
            DropForeignKey("dbo.GeoDivisions", "ParentId", "dbo.GeoDivisions");
            DropIndex("dbo.GeoDivisions", new[] { "ParentId" });
            DropIndex("dbo.Customers", new[] { "GeoDivisionId" });
            DropIndex("dbo.Invoices", new[] { "GeoDivisionId" });
            DropColumn("dbo.Customers", "GeoDivisionId");
            DropColumn("dbo.Customers", "PostalCode");
            DropColumn("dbo.Invoices", "GeoDivisionId");
            DropTable("dbo.GeoDivisions");
        }
    }
}
