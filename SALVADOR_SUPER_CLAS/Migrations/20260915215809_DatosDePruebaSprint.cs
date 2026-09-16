using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SALVADOR_SUPER_CLAS.Migrations
{
    /// <inheritdoc />
    public partial class DatosDePruebaSprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "Placa", "Capacidad" },
                values: new object[] { "CBA-2026", 40 });

            migrationBuilder.InsertData(
                table: "Salidas",
                columns: new[] { "ID_Salida", "Destino", "Fecha", "Hora", "Origen", "Placa_Vehiculo", "Tarifa" },
                values: new object[] { 1, "Santa Cruz", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 0, 0, 0), "Cochabamba", "CBA-2026", 150.00m });

            migrationBuilder.InsertData(
                table: "Asientos",
                columns: new[] { "ID_Asiento", "Estado", "ID_Salida", "Numero" },
                values: new object[,]
                {
                    { 1, "Libre", 1, 1 },
                    { 2, "Libre", 1, 2 },
                    { 3, "Libre", 1, 3 },
                    { 4, "Libre", 1, 4 },
                    { 5, "Libre", 1, 5 },
                    { 6, "Libre", 1, 6 },
                    { 7, "Libre", 1, 7 },
                    { 8, "Libre", 1, 8 },
                    { 9, "Libre", 1, 9 },
                    { 10, "Libre", 1, 10 },
                    { 11, "Libre", 1, 11 },
                    { 12, "Libre", 1, 12 },
                    { 13, "Libre", 1, 13 },
                    { 14, "Libre", 1, 14 },
                    { 15, "Libre", 1, 15 },
                    { 16, "Libre", 1, 16 },
                    { 17, "Libre", 1, 17 },
                    { 18, "Libre", 1, 18 },
                    { 19, "Libre", 1, 19 },
                    { 20, "Libre", 1, 20 },
                    { 21, "Libre", 1, 21 },
                    { 22, "Libre", 1, 22 },
                    { 23, "Libre", 1, 23 },
                    { 24, "Libre", 1, 24 },
                    { 25, "Libre", 1, 25 },
                    { 26, "Libre", 1, 26 },
                    { 27, "Libre", 1, 27 },
                    { 28, "Libre", 1, 28 },
                    { 29, "Libre", 1, 29 },
                    { 30, "Libre", 1, 30 },
                    { 31, "Libre", 1, 31 },
                    { 32, "Libre", 1, 32 },
                    { 33, "Libre", 1, 33 },
                    { 34, "Libre", 1, 34 },
                    { 35, "Libre", 1, 35 },
                    { 36, "Libre", 1, 36 },
                    { 37, "Libre", 1, 37 },
                    { 38, "Libre", 1, 38 },
                    { 39, "Libre", 1, 39 },
                    { 40, "Libre", 1, 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Salidas",
                keyColumn: "ID_Salida",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Placa",
                keyValue: "CBA-2026");
        }
    }
}
