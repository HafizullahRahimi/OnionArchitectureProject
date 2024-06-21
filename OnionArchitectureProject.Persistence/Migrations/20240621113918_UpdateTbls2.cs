using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionArchitectureProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTbls2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDateUTC",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedDateUTC",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateUTC",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateUTC",
                table: "Categories",
                type: "datetime2",
                nullable: true);
        }
    }
}
