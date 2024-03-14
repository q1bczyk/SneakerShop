using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API._Data.Migrations
{
    /// <inheritdoc />
    public partial class PostgresUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Contacts",
                newName: "Lastname");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Contacts",
                newName: "LastName");
        }
    }
}
