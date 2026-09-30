using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SALVADOR_API.Data;

#nullable disable

namespace SALVADOR_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260927224114_Sprint3_ModuloCaja")]
    partial class Sprint3_ModuloCaja
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SALVADOR_API.Models.Asiento", b =>
                {
                    b.Property<int>("ID_Asiento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ID_Asiento"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("ID_Salida")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("ID_Asiento");

                    b.HasIndex("ID_Salida");

                    b.ToTable("Asientos");

                    b.HasData(
                        new
                        {
                            ID_Asiento = 1,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 1
                        },
                        new
                        {
                            ID_Asiento = 2,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 2
                        },
                        new
                        {
                            ID_Asiento = 3,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 3
                        },
                        new
                        {
                            ID_Asiento = 4,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 4
                        },
                        new
                        {
                            ID_Asiento = 5,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 5
                        },
                        new
                        {
                            ID_Asiento = 6,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 6
                        },
                        new
                        {
                            ID_Asiento = 7,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 7
                        },
                        new
                        {
                            ID_Asiento = 8,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 8
                        },
                        new
                        {
                            ID_Asiento = 9,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 9
                        },
                        new
                        {
                            ID_Asiento = 10,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 10
                        },
                        new
                        {
                            ID_Asiento = 11,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 11
                        },
                        new
                        {
                            ID_Asiento = 12,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 12
                        },
                        new
                        {
                            ID_Asiento = 13,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 13
                        },
                        new
                        {
                            ID_Asiento = 14,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 14
                        },
                        new
                        {
                            ID_Asiento = 15,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 15
                        },
                        new
                        {
                            ID_Asiento = 16,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 16
                        },
                        new
                        {
                            ID_Asiento = 17,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 17
                        },
                        new
                        {
                            ID_Asiento = 18,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 18
                        },
                        new
                        {
                            ID_Asiento = 19,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 19
                        },
                        new
                        {
                            ID_Asiento = 20,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 20
                        },
                        new
                        {
                            ID_Asiento = 21,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 21
                        },
                        new
                        {
                            ID_Asiento = 22,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 22
                        },
                        new
                        {
                            ID_Asiento = 23,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 23
                        },
                        new
                        {
                            ID_Asiento = 24,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 24
                        },
                        new
                        {
                            ID_Asiento = 25,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 25
                        },
                        new
                        {
                            ID_Asiento = 26,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 26
                        },
                        new
                        {
                            ID_Asiento = 27,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 27
                        },
                        new
                        {
                            ID_Asiento = 28,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 28
                        },
                        new
                        {
                            ID_Asiento = 29,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 29
                        },
                        new
                        {
                            ID_Asiento = 30,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 30
                        },
                        new
                        {
                            ID_Asiento = 31,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 31
                        },
                        new
                        {
                            ID_Asiento = 32,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 32
                        },
                        new
                        {
                            ID_Asiento = 33,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 33
                        },
                        new
                        {
                            ID_Asiento = 34,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 34
                        },
                        new
                        {
                            ID_Asiento = 35,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 35
                        },
                        new
                        {
                            ID_Asiento = 36,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 36
                        },
                        new
                        {
                            ID_Asiento = 37,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 37
                        },
                        new
                        {
                            ID_Asiento = 38,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 38
                        },
                        new
                        {
                            ID_Asiento = 39,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 39
                        },
                        new
                        {
                            ID_Asiento = 40,
                            Estado = "Libre",
                            ID_Salida = 1,
                            Numero = 40
                        },
                        new
                        {
                            ID_Asiento = 10101,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 1
                        },
                        new
                        {
                            ID_Asiento = 10102,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 2
                        },
                        new
                        {
                            ID_Asiento = 10103,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 3
                        },
                        new
                        {
                            ID_Asiento = 10104,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 4
                        },
                        new
                        {
                            ID_Asiento = 10105,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 5
                        },
                        new
                        {
                            ID_Asiento = 10106,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 6
                        },
                        new
                        {
                            ID_Asiento = 10107,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 7
                        },
                        new
                        {
                            ID_Asiento = 10108,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 8
                        },
                        new
                        {
                            ID_Asiento = 10109,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 9
                        },
                        new
                        {
                            ID_Asiento = 10110,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 10
                        },
                        new
                        {
                            ID_Asiento = 10111,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 11
                        },
                        new
                        {
                            ID_Asiento = 10112,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 12
                        },
                        new
                        {
                            ID_Asiento = 10113,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 13
                        },
                        new
                        {
                            ID_Asiento = 10114,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 14
                        },
                        new
                        {
                            ID_Asiento = 10115,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 15
                        },
                        new
                        {
                            ID_Asiento = 10116,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 16
                        },
                        new
                        {
                            ID_Asiento = 10117,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 17
                        },
                        new
                        {
                            ID_Asiento = 10118,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 18
                        },
                        new
                        {
                            ID_Asiento = 10119,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 19
                        },
                        new
                        {
                            ID_Asiento = 10120,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 20
                        },
                        new
                        {
                            ID_Asiento = 10121,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 21
                        },
                        new
                        {
                            ID_Asiento = 10122,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 22
                        },
                        new
                        {
                            ID_Asiento = 10123,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 23
                        },
                        new
                        {
                            ID_Asiento = 10124,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 24
                        },
                        new
                        {
                            ID_Asiento = 10125,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 25
                        },
                        new
                        {
                            ID_Asiento = 10126,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 26
                        },
                        new
                        {
                            ID_Asiento = 10127,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 27
                        },
                        new
                        {
                            ID_Asiento = 10128,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 28
                        },
                        new
                        {
                            ID_Asiento = 10129,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 29
                        },
                        new
                        {
                            ID_Asiento = 10130,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 30
                        },
                        new
                        {
                            ID_Asiento = 10131,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 31
                        },
                        new
                        {
                            ID_Asiento = 10132,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 32
                        },
                        new
                        {
                            ID_Asiento = 10133,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 33
                        },
                        new
                        {
                            ID_Asiento = 10134,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 34
                        },
                        new
                        {
                            ID_Asiento = 10135,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 35
                        },
                        new
                        {
                            ID_Asiento = 10136,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 36
                        },
                        new
                        {
                            ID_Asiento = 10137,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 37
                        },
                        new
                        {
                            ID_Asiento = 10138,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 38
                        },
                        new
                        {
                            ID_Asiento = 10139,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 39
                        },
                        new
                        {
                            ID_Asiento = 10140,
                            Estado = "Libre",
                            ID_Salida = 101,
                            Numero = 40
                        },
                        new
                        {
                            ID_Asiento = 10201,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 1
                        },
                        new
                        {
                            ID_Asiento = 10202,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 2
                        },
                        new
                        {
                            ID_Asiento = 10203,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 3
                        },
                        new
                        {
                            ID_Asiento = 10204,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 4
                        },
                        new
                        {
                            ID_Asiento = 10205,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 5
                        },
                        new
                        {
                            ID_Asiento = 10206,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 6
                        },
                        new
                        {
                            ID_Asiento = 10207,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 7
                        },
                        new
                        {
                            ID_Asiento = 10208,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 8
                        },
                        new
                        {
                            ID_Asiento = 10209,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 9
                        },
                        new
                        {
                            ID_Asiento = 10210,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 10
                        },
                        new
                        {
                            ID_Asiento = 10211,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 11
                        },
                        new
                        {
                            ID_Asiento = 10212,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 12
                        },
                        new
                        {
                            ID_Asiento = 10213,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 13
                        },
                        new
                        {
                            ID_Asiento = 10214,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 14
                        },
                        new
                        {
                            ID_Asiento = 10215,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 15
                        },
                        new
                        {
                            ID_Asiento = 10216,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 16
                        },
                        new
                        {
                            ID_Asiento = 10217,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 17
                        },
                        new
                        {
                            ID_Asiento = 10218,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 18
                        },
                        new
                        {
                            ID_Asiento = 10219,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 19
                        },
                        new
                        {
                            ID_Asiento = 10220,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 20
                        },
                        new
                        {
                            ID_Asiento = 10221,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 21
                        },
                        new
                        {
                            ID_Asiento = 10222,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 22
                        },
                        new
                        {
                            ID_Asiento = 10223,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 23
                        },
                        new
                        {
                            ID_Asiento = 10224,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 24
                        },
                        new
                        {
                            ID_Asiento = 10225,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 25
                        },
                        new
                        {
                            ID_Asiento = 10226,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 26
                        },
                        new
                        {
                            ID_Asiento = 10227,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 27
                        },
                        new
                        {
                            ID_Asiento = 10228,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 28
                        },
                        new
                        {
                            ID_Asiento = 10229,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 29
                        },
                        new
                        {
                            ID_Asiento = 10230,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 30
                        },
                        new
                        {
                            ID_Asiento = 10231,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 31
                        },
                        new
                        {
                            ID_Asiento = 10232,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 32
                        },
                        new
                        {
                            ID_Asiento = 10233,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 33
                        },
                        new
                        {
                            ID_Asiento = 10234,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 34
                        },
                        new
                        {
                            ID_Asiento = 10235,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 35
                        },
                        new
                        {
                            ID_Asiento = 10236,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 36
                        },
                        new
                        {
                            ID_Asiento = 10237,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 37
                        },
                        new
                        {
                            ID_Asiento = 10238,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 38
                        },
                        new
                        {
                            ID_Asiento = 10239,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 39
                        },
                        new
                        {
                            ID_Asiento = 10240,
                            Estado = "Libre",
                            ID_Salida = 102,
                            Numero = 40
                        },
                        new
                        {
                            ID_Asiento = 10301,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 1
                        },
                        new
                        {
                            ID_Asiento = 10302,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 2
                        },
                        new
                        {
                            ID_Asiento = 10303,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 3
                        },
                        new
                        {
                            ID_Asiento = 10304,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 4
                        },
                        new
                        {
                            ID_Asiento = 10305,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 5
                        },
                        new
                        {
                            ID_Asiento = 10306,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 6
                        },
                        new
                        {
                            ID_Asiento = 10307,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 7
                        },
                        new
                        {
                            ID_Asiento = 10308,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 8
                        },
                        new
                        {
                            ID_Asiento = 10309,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 9
                        },
                        new
                        {
                            ID_Asiento = 10310,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 10
                        },
                        new
                        {
                            ID_Asiento = 10311,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 11
                        },
                        new
                        {
                            ID_Asiento = 10312,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 12
                        },
                        new
                        {
                            ID_Asiento = 10313,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 13
                        },
                        new
                        {
                            ID_Asiento = 10314,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 14
                        },
                        new
                        {
                            ID_Asiento = 10315,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 15
                        },
                        new
                        {
                            ID_Asiento = 10316,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 16
                        },
                        new
                        {
                            ID_Asiento = 10317,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 17
                        },
                        new
                        {
                            ID_Asiento = 10318,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 18
                        },
                        new
                        {
                            ID_Asiento = 10319,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 19
                        },
                        new
                        {
                            ID_Asiento = 10320,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 20
                        },
                        new
                        {
                            ID_Asiento = 10321,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 21
                        },
                        new
                        {
                            ID_Asiento = 10322,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 22
                        },
                        new
                        {
                            ID_Asiento = 10323,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 23
                        },
                        new
                        {
                            ID_Asiento = 10324,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 24
                        },
                        new
                        {
                            ID_Asiento = 10325,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 25
                        },
                        new
                        {
                            ID_Asiento = 10326,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 26
                        },
                        new
                        {
                            ID_Asiento = 10327,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 27
                        },
                        new
                        {
                            ID_Asiento = 10328,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 28
                        },
                        new
                        {
                            ID_Asiento = 10329,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 29
                        },
                        new
                        {
                            ID_Asiento = 10330,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 30
                        },
                        new
                        {
                            ID_Asiento = 10331,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 31
                        },
                        new
                        {
                            ID_Asiento = 10332,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 32
                        },
                        new
                        {
                            ID_Asiento = 10333,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 33
                        },
                        new
                        {
                            ID_Asiento = 10334,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 34
                        },
                        new
                        {
                            ID_Asiento = 10335,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 35
                        },
                        new
                        {
                            ID_Asiento = 10336,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 36
                        },
                        new
                        {
                            ID_Asiento = 10337,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 37
                        },
                        new
                        {
                            ID_Asiento = 10338,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 38
                        },
                        new
                        {
                            ID_Asiento = 10339,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 39
                        },
                        new
                        {
                            ID_Asiento = 10340,
                            Estado = "Libre",
                            ID_Salida = 103,
                            Numero = 40
                        });
                });

            modelBuilder.Entity("SALVADOR_API.Models.Caja", b =>
                {
                    b.Property<int>("ID_Caja")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ID_Caja"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("Fecha_Apertura")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Fecha_Cierre")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ID_Usuario")
                        .HasColumnType("int");

                    b.Property<decimal?>("Monto_Final")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Monto_Inicial")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("Total_Ventas")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ID_Caja");

                    b.HasIndex("ID_Usuario");

                    b.ToTable("Cajas");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Pasajero", b =>
                {
                    b.Property<string>("Documento")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Nacionalidad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nombre_Completo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Tipo_Documento")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Documento");

                    b.ToTable("Pasajeros");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Salida", b =>
                {
                    b.Property<int>("ID_Salida")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ID_Salida"));

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeSpan>("Hora")
                        .HasColumnType("time(6)");

                    b.Property<string>("Origen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Placa_Vehiculo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<decimal>("Tarifa")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ID_Salida");

                    b.HasIndex("Placa_Vehiculo");

                    b.ToTable("Salidas");

                    b.HasData(
                        new
                        {
                            ID_Salida = 1,
                            Destino = "Santa Cruz",
                            Fecha = new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hora = new TimeSpan(0, 20, 0, 0, 0),
                            Origen = "Cochabamba",
                            Placa_Vehiculo = "CBA-2026",
                            Tarifa = 150.00m
                        },
                        new
                        {
                            ID_Salida = 101,
                            Destino = "Arica",
                            Fecha = new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hora = new TimeSpan(0, 8, 30, 0, 0),
                            Origen = "Cochabamba",
                            Placa_Vehiculo = "LPZ-4521",
                            Tarifa = 280.00m
                        },
                        new
                        {
                            ID_Salida = 102,
                            Destino = "Iquique",
                            Fecha = new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hora = new TimeSpan(0, 19, 0, 0, 0),
                            Origen = "Santa Cruz",
                            Placa_Vehiculo = "SCZ-7788",
                            Tarifa = 350.00m
                        },
                        new
                        {
                            ID_Salida = 103,
                            Destino = "Santa Cruz",
                            Fecha = new DateTime(2026, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hora = new TimeSpan(0, 21, 0, 0, 0),
                            Origen = "Cochabamba",
                            Placa_Vehiculo = "CBA-2026",
                            Tarifa = 150.00m
                        });
                });

            modelBuilder.Entity("SALVADOR_API.Models.Usuario", b =>
                {
                    b.Property<int>("ID_Usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ID_Usuario"));

                    b.Property<string>("ContrasenaHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("ID_Usuario");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            ID_Usuario = 1,
                            ContrasenaHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9",
                            NombreCompleto = "Administrador General",
                            NombreUsuario = "admin",
                            Rol = "Administrador"
                        },
                        new
                        {
                            ID_Usuario = 2,
                            ContrasenaHash = "a54b1f38e961eba051a95855c11ce3aa04da8a8590293999915bd7a463765839",
                            NombreCompleto = "Boletero de Turno",
                            NombreUsuario = "boletero1",
                            Rol = "Boletero"
                        },
                        new
                        {
                            ID_Usuario = 3,
                            ContrasenaHash = "ecfba551324356e5bd27b548adf36b728783f60d9b573d142caac7baad62be49",
                            NombreCompleto = "Gerente de Operaciones",
                            NombreUsuario = "gerente1",
                            Rol = "GerenteOperaciones"
                        },
                        new
                        {
                            ID_Usuario = 4,
                            ContrasenaHash = "a54b1f38e961eba051a95855c11ce3aa04da8a8590293999915bd7a463765839",
                            NombreCompleto = "Segundo Boletero",
                            NombreUsuario = "boletero2",
                            Rol = "Boletero"
                        });
                });

            modelBuilder.Entity("SALVADOR_API.Models.Vehiculo", b =>
                {
                    b.Property<string>("Placa")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<int>("Capacidad")
                        .HasColumnType("int");

                    b.HasKey("Placa");

                    b.ToTable("Vehiculos");

                    b.HasData(
                        new
                        {
                            Placa = "CBA-2026",
                            Capacidad = 40
                        },
                        new
                        {
                            Placa = "LPZ-4521",
                            Capacidad = 40
                        },
                        new
                        {
                            Placa = "SCZ-7788",
                            Capacidad = 40
                        });
                });

            modelBuilder.Entity("SALVADOR_API.Models.Venta", b =>
                {
                    b.Property<int>("ID_Venta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ID_Venta"));

                    b.Property<string>("Documento_Pasajero")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("Fecha_Transaccion")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ID_Asiento")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Caja")
                        .HasColumnType("int");

                    b.Property<string>("Metodo_Pago")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Token_Boletero")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("ID_Venta");

                    b.HasIndex("Documento_Pasajero");

                    b.HasIndex("ID_Asiento")
                        .IsUnique();

                    b.HasIndex("ID_Caja");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Asiento", b =>
                {
                    b.HasOne("SALVADOR_API.Models.Salida", "Salida")
                        .WithMany("Asientos")
                        .HasForeignKey("ID_Salida")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salida");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Caja", b =>
                {
                    b.HasOne("SALVADOR_API.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("ID_Usuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Salida", b =>
                {
                    b.HasOne("SALVADOR_API.Models.Vehiculo", "Vehiculo")
                        .WithMany("Salidas")
                        .HasForeignKey("Placa_Vehiculo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Venta", b =>
                {
                    b.HasOne("SALVADOR_API.Models.Pasajero", "Pasajero")
                        .WithMany("Ventas")
                        .HasForeignKey("Documento_Pasajero")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SALVADOR_API.Models.Asiento", "Asiento")
                        .WithOne("Venta")
                        .HasForeignKey("SALVADOR_API.Models.Venta", "ID_Asiento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SALVADOR_API.Models.Caja", "Caja")
                        .WithMany("Ventas")
                        .HasForeignKey("ID_Caja")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Asiento");

                    b.Navigation("Caja");

                    b.Navigation("Pasajero");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Asiento", b =>
                {
                    b.Navigation("Venta");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Caja", b =>
                {
                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Pasajero", b =>
                {
                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Salida", b =>
                {
                    b.Navigation("Asientos");
                });

            modelBuilder.Entity("SALVADOR_API.Models.Vehiculo", b =>
                {
                    b.Navigation("Salidas");
                });
#pragma warning restore 612, 618
        }
    }
}
