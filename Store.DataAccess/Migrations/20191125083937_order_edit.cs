using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Store.DataAccess.Migrations
{
    public partial class order_edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Orders",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Count",
                table: "OrderItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
