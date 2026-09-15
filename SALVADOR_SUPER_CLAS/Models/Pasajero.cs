using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SALVADOR_SUPER_CLAS.Models
{
    public class Pasajero
    {
        [Key]
        [MaxLength(30)]
        public string Documento { get; set; } = null!;

        [MaxLength(150)]
        public string Nombre_Completo { get; set; } = null!;

        [MaxLength(50)]
        public string Nacionalidad { get; set; } = null!;

        [MaxLength(20)]
        public string Genero { get; set; } = null!;

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}