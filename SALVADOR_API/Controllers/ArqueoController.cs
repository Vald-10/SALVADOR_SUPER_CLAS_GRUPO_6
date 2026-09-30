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
    public class ArqueoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArqueoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("cajas")]
        public async Task<ActionResult<List<CierreCajaDto>>> Cajas(DateTime? fecha)
        {
            DateTime desde = fecha?.Date ?? DateTime.Today.AddDays(-30);
            DateTime hasta = fecha?.Date.AddDays(1) ?? DateTime.Today.AddDays(1);

            var cajas = await CajaCalculos.CajasConDetalle(_context)
                .Where(c => c.Fecha_Apertura >= desde && c.Fecha_Apertura < hasta)
                .OrderByDescending(c => c.Fecha_Apertura)
                .ToListAsync();

            var resultado = cajas
                .Select(c => CajaCalculos.ConstruirArqueo(c, incluirVentas: false))
                .ToList();

            return Ok(resultado);
        }
    }
}
