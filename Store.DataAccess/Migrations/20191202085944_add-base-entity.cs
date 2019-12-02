using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class addbaseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationData",
                table: "PrintingEditions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationData",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isRemoved",
                table: "Payments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationData",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isRemoved",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationData",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isRemoved",
                table: "OrderItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationData",
                table: "Authors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isRemoved",
                table: "Authors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationData",
                table: "PrintingEditions");

            migrationBuilder.DropColumn(
                name: "CreationData",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "isRemoved",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreationData",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "isRemoved",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreationData",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "isRemoved",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CreationData",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "isRemoved",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "isRemoved",
                table: "PrintingEditions",
                newName: "IsRemoved");
        }
    }
}
