using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SALVADOR_API.Models
{
    public class Pasajero
    {
        [Key]
        [Required(ErrorMessage = "El Documento es obligatorio.")]
        [MaxLength(30)]
        public string Documento { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        [MaxLength(20)]
        public string Tipo_Documento { get; set; } = "CI";

        [Required(ErrorMessage = "El Nombre Completo es obligatorio.")]
        [MaxLength(150)]
        public string Nombre_Completo { get; set; } = null!;

        [Required(ErrorMessage = "La Nacionalidad es obligatoria para el control migratorio.")]
        [MaxLength(50)]
        public string Nacionalidad { get; set; } = null!;

        [Required(ErrorMessage = "El Género es obligatorio.")]
        [MaxLength(20)]
        public string Genero { get; set; } = null!;

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
