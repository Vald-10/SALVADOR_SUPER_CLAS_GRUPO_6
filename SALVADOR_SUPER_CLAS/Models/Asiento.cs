using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALVADOR_SUPER_CLAS.Models
{
    public class Asiento
    {
        [Key]
        public int ID_Asiento { get; set; }

        public int ID_Salida { get; set; }
        public int Numero { get; set; }

        [MaxLength(20)]
        public string Estado { get; set; } = null!;

        [ForeignKey("ID_Salida")]
        public Salida Salida { get; set; } = null!;

        public Venta? Venta { get; set; }
    }
}