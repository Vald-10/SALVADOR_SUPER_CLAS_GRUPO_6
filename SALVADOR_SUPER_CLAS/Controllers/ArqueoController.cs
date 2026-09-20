using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_SUPER_CLAS.Data;
using SALVADOR_SUPER_CLAS.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SALVADOR_SUPER_CLAS.Controllers
{
    public class ArqueoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArqueoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Resumen()
        {
            string nombreCaja = "Caja Principal - Sucursal Cochabamba";
            DateTime fechaHoy = DateTime.Today;

            var ventasDelTurno = _context.Ventas
                .Where(v => v.Fecha_Transaccion.Date == fechaHoy);

            int boletosEmitidos = await ventasDelTurno.CountAsync();
            decimal recaudacionTotal = await ventasDelTurno.SumAsync(v => (decimal?)v.Monto) ?? 0m;

            var viewModel = new ArqueoViewModel
            {
                CajeroActual = nombreCaja,
                FechaCierre = DateTime.Now,
                MontoApertura = 0m,
                CantidadBoletos = boletosEmitidos,
                TotalVentas = recaudacionTotal
            };

            return View(viewModel);
        }
    }
}