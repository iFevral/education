using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class delete_keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorInBooks",
                table: "AuthorInBooks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuthorInBooks");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "AuthorInBooks");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PrintingEditions",
                type: "date",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorInBooks",
                table: "AuthorInBooks",
                columns: new[] { "AuthorId", "PrintingEditionId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorInBooks",
                table: "AuthorInBooks");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PrintingEditions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AuthorInBooks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "AuthorInBooks",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorInBooks",
                table: "AuthorInBooks",
                column: "Id");
        }
    }
}
