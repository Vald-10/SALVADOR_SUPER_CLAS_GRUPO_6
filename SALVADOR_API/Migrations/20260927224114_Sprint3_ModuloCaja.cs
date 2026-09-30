using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace SALVADOR_API.Migrations
{
    public partial class Sprint3_ModuloCaja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_Caja",
                table: "Ventas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo_Documento",
                table: "Pasajeros",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "CI")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cajas",
                columns: table => new
                {
                    ID_Caja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    Fecha_Apertura = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Monto_Inicial = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Fecha_Cierre = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Total_Ventas = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Monto_Final = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cajas", x => x.ID_Caja);
                    table.ForeignKey(
                        name: "FK_Cajas_Usuarios_ID_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_Usuario",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Salidas",
                columns: new[] { "ID_Salida", "Destino", "Fecha", "Hora", "Origen", "Placa_Vehiculo", "Tarifa" },
                values: new object[] { 103, "Santa Cruz", new DateTime(2026, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 21, 0, 0, 0), "Cochabamba", "CBA-2026", 150.00m });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "ID_Usuario", "ContrasenaHash", "NombreCompleto", "NombreUsuario", "Rol" },
                values: new object[] { 4, "a54b1f38e961eba051a95855c11ce3aa04da8a8590293999915bd7a463765839", "Segundo Boletero", "boletero2", "Boletero" });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "Placa", "Capacidad" },
                values: new object[,]
                {
                    { "LPZ-4521", 40 },
                    { "SCZ-7788", 40 }
                });

            migrationBuilder.InsertData(
                table: "Asientos",
                columns: new[] { "ID_Asiento", "Estado", "ID_Salida", "Numero" },
                values: new object[,]
                {
                    { 10301, "Libre", 103, 1 },
                    { 10302, "Libre", 103, 2 },
                    { 10303, "Libre", 103, 3 },
                    { 10304, "Libre", 103, 4 },
                    { 10305, "Libre", 103, 5 },
                    { 10306, "Libre", 103, 6 },
                    { 10307, "Libre", 103, 7 },
                    { 10308, "Libre", 103, 8 },
                    { 10309, "Libre", 103, 9 },
                    { 10310, "Libre", 103, 10 },
                    { 10311, "Libre", 103, 11 },
                    { 10312, "Libre", 103, 12 },
                    { 10313, "Libre", 103, 13 },
                    { 10314, "Libre", 103, 14 },
                    { 10315, "Libre", 103, 15 },
                    { 10316, "Libre", 103, 16 },
                    { 10317, "Libre", 103, 17 },
                    { 10318, "Libre", 103, 18 },
                    { 10319, "Libre", 103, 19 },
                    { 10320, "Libre", 103, 20 },
                    { 10321, "Libre", 103, 21 },
                    { 10322, "Libre", 103, 22 },
                    { 10323, "Libre", 103, 23 },
                    { 10324, "Libre", 103, 24 },
                    { 10325, "Libre", 103, 25 },
                    { 10326, "Libre", 103, 26 },
                    { 10327, "Libre", 103, 27 },
                    { 10328, "Libre", 103, 28 },
                    { 10329, "Libre", 103, 29 },
                    { 10330, "Libre", 103, 30 },
                    { 10331, "Libre", 103, 31 },
                    { 10332, "Libre", 103, 32 },
                    { 10333, "Libre", 103, 33 },
                    { 10334, "Libre", 103, 34 },
                    { 10335, "Libre", 103, 35 },
                    { 10336, "Libre", 103, 36 },
                    { 10337, "Libre", 103, 37 },
                    { 10338, "Libre", 103, 38 },
                    { 10339, "Libre", 103, 39 },
                    { 10340, "Libre", 103, 40 }
                });

            migrationBuilder.InsertData(
                table: "Salidas",
                columns: new[] { "ID_Salida", "Destino", "Fecha", "Hora", "Origen", "Placa_Vehiculo", "Tarifa" },
                values: new object[,]
                {
                    { 101, "Arica", new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 30, 0, 0), "Cochabamba", "LPZ-4521", 280.00m },
                    { 102, "Iquique", new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 0, 0, 0), "Santa Cruz", "SCZ-7788", 350.00m }
                });

            migrationBuilder.InsertData(
                table: "Asientos",
                columns: new[] { "ID_Asiento", "Estado", "ID_Salida", "Numero" },
                values: new object[,]
                {
                    { 10101, "Libre", 101, 1 },
                    { 10102, "Libre", 101, 2 },
                    { 10103, "Libre", 101, 3 },
                    { 10104, "Libre", 101, 4 },
                    { 10105, "Libre", 101, 5 },
                    { 10106, "Libre", 101, 6 },
                    { 10107, "Libre", 101, 7 },
                    { 10108, "Libre", 101, 8 },
                    { 10109, "Libre", 101, 9 },
                    { 10110, "Libre", 101, 10 },
                    { 10111, "Libre", 101, 11 },
                    { 10112, "Libre", 101, 12 },
                    { 10113, "Libre", 101, 13 },
                    { 10114, "Libre", 101, 14 },
                    { 10115, "Libre", 101, 15 },
                    { 10116, "Libre", 101, 16 },
                    { 10117, "Libre", 101, 17 },
                    { 10118, "Libre", 101, 18 },
                    { 10119, "Libre", 101, 19 },
                    { 10120, "Libre", 101, 20 },
                    { 10121, "Libre", 101, 21 },
                    { 10122, "Libre", 101, 22 },
                    { 10123, "Libre", 101, 23 },
                    { 10124, "Libre", 101, 24 },
                    { 10125, "Libre", 101, 25 },
                    { 10126, "Libre", 101, 26 },
                    { 10127, "Libre", 101, 27 },
                    { 10128, "Libre", 101, 28 },
                    { 10129, "Libre", 101, 29 },
                    { 10130, "Libre", 101, 30 },
                    { 10131, "Libre", 101, 31 },
                    { 10132, "Libre", 101, 32 },
                    { 10133, "Libre", 101, 33 },
                    { 10134, "Libre", 101, 34 },
                    { 10135, "Libre", 101, 35 },
                    { 10136, "Libre", 101, 36 },
                    { 10137, "Libre", 101, 37 },
                    { 10138, "Libre", 101, 38 },
                    { 10139, "Libre", 101, 39 },
                    { 10140, "Libre", 101, 40 },
                    { 10201, "Libre", 102, 1 },
                    { 10202, "Libre", 102, 2 },
                    { 10203, "Libre", 102, 3 },
                    { 10204, "Libre", 102, 4 },
                    { 10205, "Libre", 102, 5 },
                    { 10206, "Libre", 102, 6 },
                    { 10207, "Libre", 102, 7 },
                    { 10208, "Libre", 102, 8 },
                    { 10209, "Libre", 102, 9 },
                    { 10210, "Libre", 102, 10 },
                    { 10211, "Libre", 102, 11 },
                    { 10212, "Libre", 102, 12 },
                    { 10213, "Libre", 102, 13 },
                    { 10214, "Libre", 102, 14 },
                    { 10215, "Libre", 102, 15 },
                    { 10216, "Libre", 102, 16 },
                    { 10217, "Libre", 102, 17 },
                    { 10218, "Libre", 102, 18 },
                    { 10219, "Libre", 102, 19 },
                    { 10220, "Libre", 102, 20 },
                    { 10221, "Libre", 102, 21 },
                    { 10222, "Libre", 102, 22 },
                    { 10223, "Libre", 102, 23 },
                    { 10224, "Libre", 102, 24 },
                    { 10225, "Libre", 102, 25 },
                    { 10226, "Libre", 102, 26 },
                    { 10227, "Libre", 102, 27 },
                    { 10228, "Libre", 102, 28 },
                    { 10229, "Libre", 102, 29 },
                    { 10230, "Libre", 102, 30 },
                    { 10231, "Libre", 102, 31 },
                    { 10232, "Libre", 102, 32 },
                    { 10233, "Libre", 102, 33 },
                    { 10234, "Libre", 102, 34 },
                    { 10235, "Libre", 102, 35 },
                    { 10236, "Libre", 102, 36 },
                    { 10237, "Libre", 102, 37 },
                    { 10238, "Libre", 102, 38 },
                    { 10239, "Libre", 102, 39 },
                    { 10240, "Libre", 102, 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ID_Caja",
                table: "Ventas",
                column: "ID_Caja");

            migrationBuilder.CreateIndex(
                name: "IX_Cajas_ID_Usuario",
                table: "Cajas",
                column: "ID_Usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Cajas_ID_Caja",
                table: "Ventas",
                column: "ID_Caja",
                principalTable: "Cajas",
                principalColumn: "ID_Caja",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Cajas_ID_Caja",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "Cajas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_ID_Caja",
                table: "Ventas");

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10101);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10102);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10103);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10104);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10105);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10106);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10107);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10108);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10109);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10110);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10111);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10112);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10113);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10114);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10115);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10116);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10117);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10118);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10119);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10120);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10121);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10122);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10123);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10124);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10125);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10126);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10127);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10128);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10129);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10130);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10131);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10132);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10133);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10134);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10135);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10136);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10137);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10138);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10139);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10140);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10201);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10202);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10203);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10204);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10205);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10206);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10207);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10208);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10209);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10210);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10211);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10212);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10213);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10214);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10215);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10216);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10217);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10218);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10219);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10220);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10221);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10222);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10223);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10224);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10225);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10226);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10227);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10228);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10229);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10230);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10231);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10232);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10233);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10234);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10235);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10236);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10237);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10238);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10239);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10240);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10301);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10302);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10303);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10304);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10305);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10306);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10307);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10308);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10309);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10310);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10311);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10312);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10313);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10314);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10315);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10316);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10317);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10318);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10319);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10320);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10321);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10322);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10323);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10324);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10325);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10326);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10327);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10328);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10329);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10330);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10331);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10332);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10333);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10334);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10335);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10336);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10337);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10338);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10339);

            migrationBuilder.DeleteData(
                table: "Asientos",
                keyColumn: "ID_Asiento",
                keyValue: 10340);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "ID_Usuario",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Salidas",
                keyColumn: "ID_Salida",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Salidas",
                keyColumn: "ID_Salida",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Salidas",
                keyColumn: "ID_Salida",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Placa",
                keyValue: "LPZ-4521");

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Placa",
                keyValue: "SCZ-7788");

            migrationBuilder.DropColumn(
                name: "ID_Caja",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "Tipo_Documento",
                table: "Pasajeros");
        }
    }
}
