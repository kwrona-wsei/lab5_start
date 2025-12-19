using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab5_start.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToGiftRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GiftRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "GiftRequests");
        }
    }
}
