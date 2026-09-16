using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SALVADOR_SUPER_CLAS.Migrations
{
    /// <inheritdoc />
    public partial class HU02_ValidacionesMigratorias : Migration
    {
        /// <inheritdoc />
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

        /// <inheritdoc />
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
