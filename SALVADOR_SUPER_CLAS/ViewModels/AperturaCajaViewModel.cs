using System;
using System.ComponentModel.DataAnnotations;

namespace SALVADOR_SUPER_CLAS.ViewModels
{
    public class AperturaCajaViewModel
    {
        public DateTime Fecha_Apertura { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El monto inicial es obligatorio (puede ser 0).")]
        [Range(0, 100000, ErrorMessage = "El monto inicial debe estar entre 0 y 100000.")]
        [Display(Name = "Monto inicial")]
        public decimal? Monto_Inicial { get; set; }
    }
}
