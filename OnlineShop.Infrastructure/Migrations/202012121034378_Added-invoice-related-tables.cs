namespace OnlineShop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedinvoicerelatedtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        Amount = c.Long(nullable: false),
                        Description = c.String(maxLength: 2000),
                        ExtraInfo = c.String(maxLength: 255),
                        RetrievalRefNo = c.String(),
                        SystemTraceNo = c.String(),
                        Token = c.String(),
                        Url = c.String(),
                        PaymentKey = c.String(),
                        Title = c.String(),
                        PaymentAccountId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentAccounts", t => t.PaymentAccountId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.PaymentAccountId);
            
            CreateTable(
                "dbo.EPaymentLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 500),
                        LogDate = c.DateTime(nullable: false),
                        LogType = c.String(maxLength: 50),
                        PaymentKey = c.Int(nullable: false),
                        EPaymentId = c.Int(nullable: false),
                        MethodName = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                        Amount = c.Long(nullable: false),
                        RetrievalRefNo = c.String(maxLength: 50),
                        StackTraceNo = c.String(maxLength: 50),
                        Token = c.String(maxLength: 100),
                        Url = c.String(maxLength: 200),
                        ReturnObjectSerialization = c.String(),
                        ReturnUrl = c.String(maxLength: 200),
                        AdditionalData = c.String(maxLength: 500),
                        ResCode = c.String(maxLength: 10),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EPayments", t => t.EPaymentId, cascadeDelete: true)
                .Index(t => t.EPaymentId);
            
            CreateTable(
                "dbo.PaymentAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        PIN = c.String(),
                        ComebackUrl = c.String(),
                        PaymentUrl = c.String(),
                        MerchantId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Invoices", "IsPayed", c => c.Boolean(nullable: false));
            AddColumn("dbo.InvoiceItems", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.InvoiceItems", "TotalPrice", c => c.Long(nullable: false));
            AddColumn("dbo.InvoiceItems", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.InvoiceItems", "MainFeatureId", c => c.Int(nullable: false));
            AddColumn("dbo.InvoiceItems", "ProductMainFeature_Id", c => c.Int());
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            CreateIndex("dbo.InvoiceItems", "ProductId");
            CreateIndex("dbo.InvoiceItems", "ProductMainFeature_Id");
            AddForeignKey("dbo.InvoiceItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceItems", "ProductMainFeature_Id", "dbo.ProductMainFeatures", "Id");
            DropColumn("dbo.InvoiceItems", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoiceItems", "Amount", c => c.Long(nullable: false));
            DropForeignKey("dbo.InvoiceItems", "ProductMainFeature_Id", "dbo.ProductMainFeatures");
            DropForeignKey("dbo.InvoiceItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.EPayments", "PaymentAccountId", "dbo.PaymentAccounts");
            DropForeignKey("dbo.EPayments", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.EPaymentLogs", "EPaymentId", "dbo.EPayments");
            DropIndex("dbo.EPaymentLogs", new[] { "EPaymentId" });
            DropIndex("dbo.EPayments", new[] { "PaymentAccountId" });
            DropIndex("dbo.EPayments", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceItems", new[] { "ProductMainFeature_Id" });
            DropIndex("dbo.InvoiceItems", new[] { "ProductId" });
            AlterColumn("dbo.Customers", "Address", c => c.String());
            DropColumn("dbo.InvoiceItems", "ProductMainFeature_Id");
            DropColumn("dbo.InvoiceItems", "MainFeatureId");
            DropColumn("dbo.InvoiceItems", "ProductId");
            DropColumn("dbo.InvoiceItems", "TotalPrice");
            DropColumn("dbo.InvoiceItems", "Quantity");
            DropColumn("dbo.Invoices", "IsPayed");
            DropTable("dbo.PaymentAccounts");
            DropTable("dbo.EPaymentLogs");
            DropTable("dbo.EPayments");
        }
    }
}
