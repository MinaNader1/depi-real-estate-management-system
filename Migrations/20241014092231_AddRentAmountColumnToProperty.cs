using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace depi_real_state_management_system.Migrations
{
    /// <inheritdoc />
    public partial class AddRentAmountColumnToProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentAmount",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentAmount",
                table: "Properties");
        }
    }
}
