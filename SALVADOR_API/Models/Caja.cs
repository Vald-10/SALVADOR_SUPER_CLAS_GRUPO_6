using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALVADOR_API.Models
{
    public class Caja
    {
        [Key]
        public int ID_Caja { get; set; }

        [Required]
        public int ID_Usuario { get; set; }

        [Required]
        public DateTime Fecha_Apertura { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Monto_Inicial { get; set; }

        public DateTime? Fecha_Cierre { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Total_Ventas { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Monto_Final { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Abierta";

        [ForeignKey("ID_Usuario")]
        public Usuario Usuario { get; set; } = null!;

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
