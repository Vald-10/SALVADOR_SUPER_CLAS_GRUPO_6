using System.ComponentModel.DataAnnotations;

namespace SALVADOR_SUPER_CLAS.ViewModels
{
    public class VentaPresencialViewModel
    {
        [Required]
        public int ID_Asiento { get; set; }

        public int NumeroAsiento { get; set; }

        [Required(ErrorMessage = "El Documento (CI/Pasaporte) es obligatorio.")]
        [MaxLength(30)]
        public string Documento { get; set; } = null!;

        [Required(ErrorMessage = "El Nombre Completo es obligatorio.")]
        [MaxLength(150)]
        public string Nombre_Completo { get; set; } = null!;

        [Required(ErrorMessage = "La Nacionalidad es requerida por migración.")]
        [MaxLength(50)]
        public string Nacionalidad { get; set; } = null!;

        [Required(ErrorMessage = "El Género es obligatorio.")]
        [MaxLength(20)]
        public string Genero { get; set; } = null!;
    }
}