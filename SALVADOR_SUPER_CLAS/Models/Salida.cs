using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALVADOR_SUPER_CLAS.Models
{
    public class Salida
    {
        [Key]
        public int ID_Salida { get; set; }

        [Required]
        [MaxLength(15)]
        public string Placa_Vehiculo { get; set; } = null!;

        [MaxLength(100)]
        public string Origen { get; set; } = null!;

        [MaxLength(100)]
        public string Destino { get; set; } = null!;

        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Tarifa { get; set; }

        [ForeignKey("Placa_Vehiculo")]
        public Vehiculo Vehiculo { get; set; } = null!;

        public ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();
    }
}