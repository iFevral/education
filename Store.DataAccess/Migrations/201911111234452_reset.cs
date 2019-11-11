namespace Store.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reset : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AuthorInBooks", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.AuthorInBooks", "PrintingEditionId", "dbo.PrintingEditions");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "PrintingEditionId", "dbo.PrintingEditions");
            DropIndex("dbo.AuthorInBooks", new[] { "AuthorId" });
            DropIndex("dbo.AuthorInBooks", new[] { "PrintingEditionId" });
            DropIndex("dbo.OrderItems", new[] { "PrintingEditionId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "PaymentId" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropTable("dbo.AuthorInBooks");
            DropTable("dbo.Authors");
            DropTable("dbo.PrintingEditions");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Payments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Currency = c.String(),
                        PrintingEditionId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrintingEditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsRemoved = c.Boolean(nullable: false),
                        Status = c.String(),
                        Currency = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthorInBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        PrintingEditionId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Orders", "User_Id");
            CreateIndex("dbo.Orders", "PaymentId");
            CreateIndex("dbo.OrderItems", "OrderId");
            CreateIndex("dbo.OrderItems", "PrintingEditionId");
            CreateIndex("dbo.AuthorInBooks", "PrintingEditionId");
            CreateIndex("dbo.AuthorInBooks", "AuthorId");
            AddForeignKey("dbo.OrderItems", "PrintingEditionId", "dbo.PrintingEditions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Orders", "PaymentId", "dbo.Payments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AuthorInBooks", "PrintingEditionId", "dbo.PrintingEditions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AuthorInBooks", "AuthorId", "dbo.Authors", "Id", cascadeDelete: true);
        }
    }
}
