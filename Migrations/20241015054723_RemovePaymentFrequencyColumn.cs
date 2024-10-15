using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace depi_real_state_management_system.Migrations
{
    /// <inheritdoc />
    public partial class RemovePaymentFrequencyColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentFrequency",
                table: "Leases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentFrequency",
                table: "Leases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
