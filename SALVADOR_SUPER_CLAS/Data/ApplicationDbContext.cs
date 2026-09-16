using Microsoft.EntityFrameworkCore;
using SALVADOR_SUPER_CLAS.Models;
using System;
using System.Collections.Generic;

namespace SALVADOR_SUPER_CLAS.Data
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

            modelBuilder.Entity<Vehiculo>().HasData(
                new Vehiculo { Placa = "CBA-2026", Capacidad = 40 }
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
            modelBuilder.Entity<Asiento>().HasData(asientos);
        }
    }
}