using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Models;
using System;
using System.Collections.Generic;

namespace SALVADOR_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Salida> Salidas { get; set; }
        public DbSet<Asiento> Asientos { get; set; }
        public DbSet<Pasajero> Pasajeros { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Caja> Cajas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Asiento)
                .WithOne(a => a.Venta)
                .HasForeignKey<Venta>(v => v.ID_Asiento);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Pasajero)
                .WithMany(p => p.Ventas)
                .HasForeignKey(v => v.Documento_Pasajero)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Caja)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ID_Caja)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Caja>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.ID_Usuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehiculo>().HasData(
                new Vehiculo { Placa = "CBA-2026", Capacidad = 40 },
                new Vehiculo { Placa = "LPZ-4521", Capacidad = 40 },
                new Vehiculo { Placa = "SCZ-7788", Capacidad = 40 }
            );

            modelBuilder.Entity<Salida>().HasData(
                new Salida
                {
                    ID_Salida = 1,
                    Placa_Vehiculo = "CBA-2026",
                    Origen = "Cochabamba",
                    Destino = "Santa Cruz",
                    Fecha = new DateTime(2026, 9, 20),
                    Hora = new TimeSpan(20, 0, 0),
                    Tarifa = 150.00m
                },
                new Salida
                {
                    ID_Salida = 101,
                    Placa_Vehiculo = "LPZ-4521",
                    Origen = "Cochabamba",
                    Destino = "Arica",
                    Fecha = new DateTime(2026, 11, 10),
                    Hora = new TimeSpan(8, 30, 0),
                    Tarifa = 280.00m
                },
                new Salida
                {
                    ID_Salida = 102,
                    Placa_Vehiculo = "SCZ-7788",
                    Origen = "Santa Cruz",
                    Destino = "Iquique",
                    Fecha = new DateTime(2026, 11, 20),
                    Hora = new TimeSpan(19, 0, 0),
                    Tarifa = 350.00m
                },
                new Salida
                {
                    ID_Salida = 103,
                    Placa_Vehiculo = "CBA-2026",
                    Origen = "Cochabamba",
                    Destino = "Santa Cruz",
                    Fecha = new DateTime(2026, 12, 5),
                    Hora = new TimeSpan(21, 0, 0),
                    Tarifa = 150.00m
                }
            );

            var asientos = new List<Asiento>();
            for (int i = 1; i <= 40; i++)
            {
                asientos.Add(new Asiento
                {
                    ID_Asiento = i,
                    ID_Salida = 1,
                    Numero = i,
                    Estado = "Libre"
                });
            }

            foreach (int idSalida in new[] { 101, 102, 103 })
            {
                for (int numero = 1; numero <= 40; numero++)
                {
                    asientos.Add(new Asiento
                    {
                        ID_Asiento = idSalida * 100 + numero,
                        ID_Salida = idSalida,
                        Numero = numero,
                        Estado = "Libre"
                    });
                }
            }
            modelBuilder.Entity<Asiento>().HasData(asientos);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    ID_Usuario = 1,
                    NombreUsuario = "admin",
                    NombreCompleto = "Administrador General",
                    ContrasenaHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9",
                    Rol = "Administrador"
                },
                new Usuario
                {
                    ID_Usuario = 2,
                    NombreUsuario = "boletero1",
                    NombreCompleto = "Boletero de Turno",
                    ContrasenaHash = "a54b1f38e961eba051a95855c11ce3aa04da8a8590293999915bd7a463765839",
                    Rol = "Boletero"
                },
                new Usuario
                {
                    ID_Usuario = 3,
                    NombreUsuario = "gerente1",
                    NombreCompleto = "Gerente de Operaciones",
                    ContrasenaHash = "ecfba551324356e5bd27b548adf36b728783f60d9b573d142caac7baad62be49",
                    Rol = "GerenteOperaciones"
                },
                new Usuario
                {
                    ID_Usuario = 4,
                    NombreUsuario = "boletero2",
                    NombreCompleto = "Segundo Boletero",
                    ContrasenaHash = "a54b1f38e961eba051a95855c11ce3aa04da8a8590293999915bd7a463765839",
                    Rol = "Boletero"
                }
            );
        }
    }
}