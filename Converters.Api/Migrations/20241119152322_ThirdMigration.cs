using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Converters.Api.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Converters_Categories_CategoryId",
                table: "Converters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Converters",
                table: "Converters");

            migrationBuilder.RenameTable(
                name: "Converters",
                newName: "Services");

            migrationBuilder.RenameIndex(
                name: "IX_Converters_CategoryId",
                table: "Services",
                newName: "IX_Services_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Categories_CategoryId",
                table: "Services",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Categories_CategoryId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Converters");

            migrationBuilder.RenameIndex(
                name: "IX_Services_CategoryId",
                table: "Converters",
                newName: "IX_Converters_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Converters",
                table: "Converters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Converters_Categories_CategoryId",
                table: "Converters",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
