using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionArchitectureProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTblCategoriesAndProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedDateUtc",
                table: "Products",
                newName: "ModifiedUtcDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDateUtc",
                table: "Products",
                newName: "CreatedUtcDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDateUtc",
                table: "Categories",
                newName: "ModifiedUtcDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDateUtc",
                table: "Categories",
                newName: "CreatedUtcDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedUtcDate",
                table: "Products",
                newName: "ModifiedDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedUtcDate",
                table: "Products",
                newName: "CreatedDateUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedUtcDate",
                table: "Categories",
                newName: "ModifiedDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedUtcDate",
                table: "Categories",
                newName: "CreatedDateUtc");
        }
    }
}
