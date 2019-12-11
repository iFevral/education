using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class renametable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorInBooks_Authors_AuthorId",
                table: "AuthorInBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorInBooks_PrintingEditions_PrintingEditionId",
                table: "AuthorInBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorInBooks",
                table: "AuthorInBooks");

            migrationBuilder.RenameTable(
                name: "AuthorInBooks",
                newName: "AuthorInPrintingEdition");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorInBooks_PrintingEditionId",
                table: "AuthorInPrintingEdition",
                newName: "IX_AuthorInPrintingEdition_PrintingEditionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorInBooks_AuthorId",
                table: "AuthorInPrintingEdition",
                newName: "IX_AuthorInPrintingEdition_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorInPrintingEdition",
                table: "AuthorInPrintingEdition",
                columns: new[] { "AuthorId", "PrintingEditionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorInPrintingEdition_Authors_AuthorId",
                table: "AuthorInPrintingEdition",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorInPrintingEdition_PrintingEditions_PrintingEditionId",
                table: "AuthorInPrintingEdition",
                column: "PrintingEditionId",
                principalTable: "PrintingEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorInPrintingEdition_Authors_AuthorId",
                table: "AuthorInPrintingEdition");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorInPrintingEdition_PrintingEditions_PrintingEditionId",
                table: "AuthorInPrintingEdition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorInPrintingEdition",
                table: "AuthorInPrintingEdition");

            migrationBuilder.RenameTable(
                name: "AuthorInPrintingEdition",
                newName: "AuthorInBooks");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorInPrintingEdition_PrintingEditionId",
                table: "AuthorInBooks",
                newName: "IX_AuthorInBooks_PrintingEditionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorInPrintingEdition_AuthorId",
                table: "AuthorInBooks",
                newName: "IX_AuthorInBooks_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorInBooks",
                table: "AuthorInBooks",
                columns: new[] { "AuthorId", "PrintingEditionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorInBooks_Authors_AuthorId",
                table: "AuthorInBooks",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorInBooks_PrintingEditions_PrintingEditionId",
                table: "AuthorInBooks",
                column: "PrintingEditionId",
                principalTable: "PrintingEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
