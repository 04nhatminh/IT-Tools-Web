using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT_Tools_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddIsEnabledAndIsPremiumToTool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Tools",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Tools",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Tools");
        }
    }
}
