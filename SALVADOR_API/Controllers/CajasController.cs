using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Data;
using SALVADOR_API.Dtos;
using SALVADOR_API.Models;
using SALVADOR_API.Utils;
using System;
using System.Threading.Tasks;

namespace SALVADOR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CajasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CajasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("actual/{idUsuario}")]
        public async Task<ActionResult<CajaDto>> CajaDeHoy(int idUsuario)
        {
            var hoy = DateTime.Today;
            var caja = await _context.Cajas
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.ID_Usuario == idUsuario
                                       && c.Fecha_Apertura >= hoy
                                       && c.Fecha_Apertura < hoy.AddDays(1));

            if (caja == null)
            {
                return NotFound("El boletero todavía no abrió su caja hoy.");
            }

            return Ok(CajaCalculos.ConstruirCaja(caja));
        }

        [HttpPost("apertura")]
        public async Task<ActionResult<CajaDto>> Apertura(AperturaCajaDto request)
        {
            var usuario = await _context.Usuarios.FindAsync(request.ID_Usuario);
            if (usuario == null || usuario.Rol != "Boletero")
            {
                return BadRequest("Solo un boletero puede abrir una caja.");
            }

            var hoy = DateTime.Today;
            if (request.Fecha_Apertura.Date != hoy)
            {
                return BadRequest("La apertura de caja debe hacerse con la fecha del día.");
            }

            bool yaTieneCaja = await _context.Cajas.AnyAsync(c => c.ID_Usuario == request.ID_Usuario
                                                               && c.Fecha_Apertura >= hoy
                                                               && c.Fecha_Apertura < hoy.AddDays(1));
            if (yaTieneCaja)
            {
                return BadRequest("Ya existe una caja registrada hoy para este boletero.");
            }

            var caja = new Caja
            {
                ID_Usuario = request.ID_Usuario,
                Fecha_Apertura = DateTime.Now,
                Monto_Inicial = request.Monto_Inicial,
                Estado = "Abierta"
            };

            _context.Cajas.Add(caja);
            await _context.SaveChangesAsync();

            caja.Usuario = usuario;
            return Ok(CajaCalculos.ConstruirCaja(caja));
        }

        [HttpGet("{id}/arqueo")]
        public async Task<ActionResult<CierreCajaDto>> Arqueo(int id)
        {
            var caja = await CajaCalculos.CajasConDetalle(_context)
                .FirstOrDefaultAsync(c => c.ID_Caja == id);

            if (caja == null)
            {
                return NotFound("La caja no existe.");
            }

            return Ok(CajaCalculos.ConstruirArqueo(caja, incluirVentas: true));
        }

        [HttpPost("{id}/cierre")]
        public async Task<ActionResult<CierreCajaDto>> Cierre(int id, CerrarCajaRequestDto request)
        {
            var caja = await CajaCalculos.CajasConDetalle(_context)
                .FirstOrDefaultAsync(c => c.ID_Caja == id);

            if (caja == null)
            {
                return NotFound("La caja no existe.");
            }

            if (caja.ID_Usuario != request.ID_Usuario)
            {
                return BadRequest("Solo el boletero dueño de la caja puede cerrarla.");
            }

            if (caja.Estado == "Cerrada")
            {
                return BadRequest("Esta caja ya fue cerrada.");
            }

            var arqueo = CajaCalculos.ConstruirArqueo(caja, incluirVentas: true);

            caja.Estado = "Cerrada";
            caja.Fecha_Cierre = DateTime.Now;
            caja.Total_Ventas = arqueo.Total_Ventas;
            caja.Monto_Final = arqueo.Monto_Final;
            await _context.SaveChangesAsync();

            arqueo.Estado = caja.Estado;
            arqueo.Fecha_Cierre = caja.Fecha_Cierre;
            return Ok(arqueo);
        }
    }
}
