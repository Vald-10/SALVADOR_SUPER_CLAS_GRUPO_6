using System;
using System.ComponentModel.DataAnnotations;

namespace SALVADOR_SUPER_CLAS.ViewModels
{
    public class VentaPresencialViewModel
    {
        [Required]
        public int ID_Asiento { get; set; }

        [Required]
        public int ID_Salida { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo de documento.")]
        [Display(Name = "Tipo de documento")]
        public string Tipo_Documento { get; set; } = "CI";

        [Required(ErrorMessage = "El número de documento (CI/RUT/Pasaporte) es obligatorio.")]
        [MaxLength(30)]
        [Display(Name = "Número de documento")]
        public string Documento { get; set; } = null!;

        [Required(ErrorMessage = "El Nombre Completo es obligatorio.")]
        [MaxLength(150)]
        [Display(Name = "Nombre completo")]
        public string Nombre_Completo { get; set; } = null!;

        [Required(ErrorMessage = "La Nacionalidad es requerida por migración.")]
        [MaxLength(50)]
        public string Nacionalidad { get; set; } = null!;

        [Required(ErrorMessage = "El Género es obligatorio.")]
        [MaxLength(20)]
        [Display(Name = "Género")]
        public string Genero { get; set; } = null!;

        [Required(ErrorMessage = "Seleccione el método de pago.")]
        [Display(Name = "Método de pago")]
        public string Metodo_Pago { get; set; } = "Efectivo";

        public int NumeroAsiento { get; set; }
        public string Origen { get; set; } = "";
        public string Destino { get; set; } = "";
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Tarifa { get; set; }
        public string Placa_Vehiculo { get; set; } = "";
    }
}
