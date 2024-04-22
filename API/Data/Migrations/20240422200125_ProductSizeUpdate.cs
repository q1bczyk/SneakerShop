using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductSizeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Stocks\" ALTER COLUMN \"Size\" TYPE real USING (\"Size\"::real)");
            migrationBuilder.AlterColumn<float>(
                name: "Size",
                table: "Stocks",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Stocks",
                type: "text",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
