using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace depi_real_state_management_system.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTenantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Tenants_TenantID1",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Tenants_TenantID1",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Payments_TenantID1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Leases_TenantID1",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "TenantID1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TenantID1",
                table: "Leases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantID1",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantID1",
                table: "Leases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TenantID1",
                table: "Payments",
                column: "TenantID1");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_TenantID1",
                table: "Leases",
                column: "TenantID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Tenants_TenantID1",
                table: "Leases",
                column: "TenantID1",
                principalTable: "Tenants",
                principalColumn: "TenantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Tenants_TenantID1",
                table: "Payments",
                column: "TenantID1",
                principalTable: "Tenants",
                principalColumn: "TenantID");
        }
    }
}
