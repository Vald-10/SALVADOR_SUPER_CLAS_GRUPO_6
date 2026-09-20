using System;

namespace SALVADOR_SUPER_CLAS.ViewModels
{
    public class ArqueoViewModel
    {
        public string CajeroActual { get; set; } = null!;
        public DateTime FechaCierre { get; set; }
        public decimal MontoApertura { get; set; }
        public int CantidadBoletos { get; set; }
        public decimal TotalVentas { get; set; }

        public decimal TotalEsperadoEnCaja => MontoApertura + TotalVentas;
    }
}