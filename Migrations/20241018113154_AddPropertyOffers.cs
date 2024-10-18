using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace depi_real_state_management_system.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowsLuggageDropoff",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBackyard",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDedicatedWorkspace",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDryer",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHairDryer",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasIndoorFireplace",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasKitchen",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLockOnBedroomDoor",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasWasher",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasWifi",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowsLuggageDropoff",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "HasBackyard",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "HasDedicatedWorkspace",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "HasDryer",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "HasHairDryer",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "HasIndoorFireplace",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "HasKitchen",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "HasLockOnBedroomDoor",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "HasWasher",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "HasWifi",
                table: "Properties");
        }
    }
}
