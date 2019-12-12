using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class addbaseentitytoaib : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "AuthorInPrintingEdition",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "AuthorInPrintingEdition",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "isRemoved",
                table: "AuthorInPrintingEdition",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AuthorInPrintingEdition_AuthorId_Id_PrintingEditionId",
                table: "AuthorInPrintingEdition",
                columns: new[] { "AuthorId", "Id", "PrintingEditionId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_AuthorInPrintingEdition_AuthorId_Id_PrintingEditionId",
                table: "AuthorInPrintingEdition");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AuthorInPrintingEdition");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuthorInPrintingEdition");

            migrationBuilder.DropColumn(
                name: "isRemoved",
                table: "AuthorInPrintingEdition");
        }
    }
}
