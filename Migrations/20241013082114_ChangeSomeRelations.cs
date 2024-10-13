using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace depi_real_state_management_system.Migrations
{
    public partial class ChangeSomeRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Tenants_TenantID",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Tenants_TenantID",
                table: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "TenantID",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TenantID1",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenantID",
                table: "Leases",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TenantID1",
                table: "Leases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TenantID1",
                table: "Payments",
                column: "TenantID1");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_TenantID1",
                table: "Leases",
                column: "TenantID1");

            // Modify the cascade behavior for this foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Leases_AspNetUsers_TenantID",
                table: "Leases",
                column: "TenantID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);  // Changed from Cascade to Restrict

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Tenants_TenantID1",
                table: "Leases",
                column: "TenantID1",
                principalTable: "Tenants",
                principalColumn: "TenantID");

            // Modify the cascade behavior for this foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_TenantID",
                table: "Payments",
                column: "TenantID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);  // Changed from Cascade to Restrict

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Tenants_TenantID1",
                table: "Payments",
                column: "TenantID1",
                principalTable: "Tenants",
                principalColumn: "TenantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_AspNetUsers_TenantID",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Tenants_TenantID1",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_TenantID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Tenants_TenantID1",
                table: "Payments");

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

            migrationBuilder.AlterColumn<int>(
                name: "TenantID",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "TenantID",
                table: "Leases",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Tenants_TenantID",
                table: "Leases",
                column: "TenantID",
                principalTable: "Tenants",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Tenants_TenantID",
                table: "Payments",
                column: "TenantID",
                principalTable: "Tenants",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
