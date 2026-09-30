using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Data;
using SALVADOR_API.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace SALVADOR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManifiestoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ManifiestoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idSalida}")]
        public async Task<ActionResult<ManifiestoDto>> GetManifiesto(int idSalida)
        {
            var salida = await _context.Salidas
                .Include(s => s.Asientos)
                    .ThenInclude(a => a.Venta)
                        .ThenInclude(v => v!.Pasajero)
                .FirstOrDefaultAsync(s => s.ID_Salida == idSalida);

            if (salida == null)
            {
                return NotFound("El viaje solicitado no existe.");
            }

            var dto = new ManifiestoDto
            {
                ID_Salida = salida.ID_Salida,
                Placa_Vehiculo = salida.Placa_Vehiculo,
                Capacidad = salida.Asientos.Count,
                Origen = salida.Origen,
                Destino = salida.Destino,
                Fecha = salida.Fecha,
                Hora = salida.Hora,
                Pasajeros = salida.Asientos
                    .Where(a => a.Venta != null)
                    .OrderBy(a => a.Numero)
                    .Select(a => new PasajeroManifiestoDto
                    {
                        NumeroAsiento = a.Numero,
                        Tipo_Documento = a.Venta!.Pasajero.Tipo_Documento,
                        Documento = a.Venta.Pasajero.Documento,
                        Nombre_Completo = a.Venta.Pasajero.Nombre_Completo,
                        Nacionalidad = a.Venta.Pasajero.Nacionalidad,
                        Genero = a.Venta.Pasajero.Genero
                    }).ToList()
            };

            return Ok(dto);
        }
    }
}
