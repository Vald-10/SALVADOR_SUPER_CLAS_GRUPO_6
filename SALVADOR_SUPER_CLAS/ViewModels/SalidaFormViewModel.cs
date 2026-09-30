using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SALVADOR_SUPER_CLAS.ViewModels
{
    public class SalidaFormViewModel
    {
        public int? ID_Salida { get; set; }

        [Required(ErrorMessage = "Elige el bus asignado.")]
        [Display(Name = "Bus asignado")]
        public string Placa_Vehiculo { get; set; } = null!;

        [Required(ErrorMessage = "El origen es obligatorio.")]
        [MaxLength(100)]
        public string Origen { get; set; } = null!;

        [Required(ErrorMessage = "El destino es obligatorio.")]
        [MaxLength(100)]
        public string Destino { get; set; } = null!;

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "La hora es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; } = new TimeSpan(8, 0, 0);

        [Required(ErrorMessage = "La tarifa es obligatoria.")]
        [Range(0.01, 100000, ErrorMessage = "La tarifa debe ser mayor a 0.")]
        [Display(Name = "Precio del pasaje")]
        public decimal? Tarifa { get; set; }

        public List<VehiculoOptionViewModel> BusesDisponibles { get; set; } = new List<VehiculoOptionViewModel>();

        public int Asientos_Vendidos { get; set; }
    }

    public class VehiculoOptionViewModel
    {
        public string Placa { get; set; } = null!;
        public int Capacidad { get; set; }
    }
}
