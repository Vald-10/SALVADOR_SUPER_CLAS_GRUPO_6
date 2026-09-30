using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Data;
using SALVADOR_API.Dtos;
using SALVADOR_API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALVADOR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("resumen")]
        public async Task<ActionResult<ReporteDto>> Resumen()
        {
            DateTime hoy = DateTime.Today;
            var reporte = new ReporteDto();

            reporte.Boletos_Vendidos = await _context.Ventas.CountAsync();
            reporte.Ingresos_Totales = await _context.Ventas.SumAsync(v => (decimal?)v.Monto) ?? 0m;
            reporte.Total_Efectivo = await _context.Ventas.Where(v => v.Metodo_Pago == "Efectivo").SumAsync(v => (decimal?)v.Monto) ?? 0m;
            reporte.Total_QR = await _context.Ventas.Where(v => v.Metodo_Pago == "QR").SumAsync(v => (decimal?)v.Monto) ?? 0m;
            reporte.Salidas_Programadas = await _context.Salidas.CountAsync(s => s.Fecha >= hoy);
            reporte.Cajas_Abiertas = await _context.Cajas.CountAsync(c => c.Estado == "Abierta");

            DateTime desde = hoy.AddDays(-6);
            var ventasSemana = await _context.Ventas
                .Where(v => v.Fecha_Transaccion >= desde)
                .Select(v => new { v.Fecha_Transaccion, v.Monto })
                .ToListAsync();

            for (int i = 0; i < 7; i++)
            {
                DateTime dia = desde.AddDays(i);
                var ventasDelDia = ventasSemana.Where(v => v.Fecha_Transaccion.Date == dia).ToList();
                reporte.Ingresos_Ultimos_Dias.Add(new IngresoDiaDto
                {
                    Fecha = dia,
                    Boletos = ventasDelDia.Count,
                    Total = ventasDelDia.Sum(v => v.Monto)
                });
            }

            reporte.Ocupacion = await _context.Salidas
                .OrderByDescending(s => s.Fecha)
                .ThenByDescending(s => s.Hora)
                .Take(10)
                .Select(s => new OcupacionSalidaDto
                {
                    ID_Salida = s.ID_Salida,
                    Ruta = s.Origen + " - " + s.Destino,
                    Fecha = s.Fecha,
                    Hora = s.Hora,
                    Placa_Vehiculo = s.Placa_Vehiculo,
                    Capacidad = s.Asientos.Count(),
                    Vendidos = s.Asientos.Count(a => a.Venta != null),
                    Ingresos = s.Asientos.Where(a => a.Venta != null).Sum(a => (decimal?)a.Venta!.Monto) ?? 0m
                })
                .ToListAsync();

            var cajasCerradas = await CajaCalculos.CajasConDetalle(_context)
                .Where(c => c.Estado == "Cerrada")
                .OrderByDescending(c => c.Fecha_Apertura)
                .Take(15)
                .ToListAsync();

            reporte.Historial_Cajas = cajasCerradas
                .Select(c => CajaCalculos.ConstruirArqueo(c, incluirVentas: false))
                .ToList();

            return Ok(reporte);
        }
    }
}
