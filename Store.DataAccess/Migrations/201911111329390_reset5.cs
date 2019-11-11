namespace Store.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reset5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AuthorInBooks", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.AuthorInBooks", "PrintingEditionId", "dbo.PrintingEditions");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "PrintingEditionId", "dbo.PrintingEditions");
            DropForeignKey("dbo.UserInRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.AuthorInBooks", new[] { "AuthorId" });
            DropIndex("dbo.AuthorInBooks", new[] { "PrintingEditionId" });
            DropIndex("dbo.OrderItems", new[] { "PrintingEditionId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "PaymentId" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.UserInRoles", new[] { "UserId" });
            DropIndex("dbo.UserInRoles", new[] { "RoleId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropTable("dbo.AuthorInBooks");
            DropTable("dbo.Authors");
            DropTable("dbo.PrintingEditions");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Payments");
            DropTable("dbo.Users");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.UserInRoles");
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateIndex("dbo.Roles", "Name", unique: true, name: "RoleNameIndex");
            CreateIndex("dbo.UserInRoles", "RoleId");
            CreateIndex("dbo.UserInRoles", "UserId");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.Users", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Orders", "User_Id");
            CreateIndex("dbo.Orders", "PaymentId");
            CreateIndex("dbo.OrderItems", "OrderId");
            CreateIndex("dbo.OrderItems", "PrintingEditionId");
            CreateIndex("dbo.AuthorInBooks", "PrintingEditionId");
            CreateIndex("dbo.AuthorInBooks", "AuthorId");
            AddForeignKey("dbo.UserInRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "PrintingEditionId", "dbo.PrintingEditions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserInRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "PaymentId", "dbo.Payments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AuthorInBooks", "PrintingEditionId", "dbo.PrintingEditions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AuthorInBooks", "AuthorId", "dbo.Authors", "Id", cascadeDelete: true);
        }
    }
}
