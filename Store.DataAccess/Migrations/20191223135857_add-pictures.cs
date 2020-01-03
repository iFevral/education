using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class addpictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "PrintingEditions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "PrintingEditions");
        }
    }
}
