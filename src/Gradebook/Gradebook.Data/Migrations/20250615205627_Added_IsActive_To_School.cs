using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_IsActive_To_School : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Schools",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Schools");
        }
    }
}
