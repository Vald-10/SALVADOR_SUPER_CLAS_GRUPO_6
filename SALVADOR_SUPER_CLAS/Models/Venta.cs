using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALVADOR_SUPER_CLAS.Models
{
    public class Venta
    {
        [Key]
        public int ID_Venta { get; set; }

        public int ID_Asiento { get; set; }

        [Required]
        [MaxLength(30)]
        public string Documento_Pasajero { get; set; } = null!;

        public DateTime Fecha_Transaccion { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Monto { get; set; }

        [MaxLength(20)]
        public string Metodo_Pago { get; set; } = null!;

        [MaxLength(255)]
        public string Token_Boletero { get; set; } = null!;

        [ForeignKey("ID_Asiento")]
        public Asiento Asiento { get; set; } = null!;

        [ForeignKey("Documento_Pasajero")]
        public Pasajero Pasajero { get; set; } = null!;
    }
}