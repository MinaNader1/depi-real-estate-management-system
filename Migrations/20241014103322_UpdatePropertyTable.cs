using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace depi_real_state_management_system.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "RentAmount",
                table: "Leases");

            migrationBuilder.RenameColumn(
                name: "RentAmount",
                table: "Properties",
                newName: "PricePerNight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerNight",
                table: "Properties",
                newName: "RentAmount");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RentAmount",
                table: "Leases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
