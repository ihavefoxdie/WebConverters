using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Converters.Api.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Converter");

            migrationBuilder.CreateTable(
                name: "Converters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Converters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Converters_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Converters",
                columns: new[] { "Id", "Address", "CategoryId", "Description", "Name" },
                values: new object[] { 1, "0.0.0.0", 1, "This is a test!\nThis is a test of the database and the app!", "Placeholder" });

            migrationBuilder.CreateIndex(
                name: "IX_Converters_CategoryId",
                table: "Converters",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Converters");

            migrationBuilder.CreateTable(
                name: "Converter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Converter", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Converter",
                columns: new[] { "Id", "Address", "CategoryId", "Description", "Name" },
                values: new object[] { 1, "0.0.0.0", 1, "This is a test!\nThis is a test of the database and the app!", "Placeholder" });
        }
    }
}
