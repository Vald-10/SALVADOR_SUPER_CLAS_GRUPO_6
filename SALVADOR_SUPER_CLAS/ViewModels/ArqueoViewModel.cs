using SALVADOR_SUPER_CLAS.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SALVADOR_SUPER_CLAS.ViewModels
{
    public class ArqueoViewModel
    {
        public DateTime? Fecha { get; set; }

        public List<CierreCajaDto> Cajas { get; set; } = new List<CierreCajaDto>();

        public int CajasAbiertas => Cajas.Count(c => c.EstaAbierta);
        public int CajasCerradas => Cajas.Count(c => !c.EstaAbierta);
        public int TotalBoletos => Cajas.Sum(c => c.Cantidad_Boletos);
        public decimal TotalVentas => Cajas.Sum(c => c.Total_Ventas);
        public decimal TotalEfectivo => Cajas.Sum(c => c.Total_Efectivo);
        public decimal TotalQR => Cajas.Sum(c => c.Total_QR);
    }
}
