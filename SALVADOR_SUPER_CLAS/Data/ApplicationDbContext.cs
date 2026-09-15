using Microsoft.EntityFrameworkCore;
using SALVADOR_SUPER_CLAS.Models;
using System.Reflection.Emit;

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

            // Relación estricta de 1 a 1 para evitar sobreventa
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Asiento)
                .WithOne(a => a.Venta)
                .HasForeignKey<Venta>(v => v.ID_Asiento);
        }
    }
}