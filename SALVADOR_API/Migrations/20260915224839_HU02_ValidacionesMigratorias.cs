using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SALVADOR_API.Migrations
{
    public partial class HU02_ValidacionesMigratorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Pasajeros_Documento_Pasajero",
                table: "Ventas");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Pasajeros_Documento_Pasajero",
                table: "Ventas",
                column: "Documento_Pasajero",
                principalTable: "Pasajeros",
                principalColumn: "Documento",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Pasajeros_Documento_Pasajero",
                table: "Ventas");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Pasajeros_Documento_Pasajero",
                table: "Ventas",
                column: "Documento_Pasajero",
                principalTable: "Pasajeros",
                principalColumn: "Documento",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
