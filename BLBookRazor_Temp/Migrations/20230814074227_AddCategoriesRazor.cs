using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLBookRazor_Temp.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesRazor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "CategoriesRazor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriesRazor",
                table: "CategoriesRazor",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriesRazor",
                table: "CategoriesRazor");

            migrationBuilder.RenameTable(
                name: "CategoriesRazor",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");
        }
    }
}
