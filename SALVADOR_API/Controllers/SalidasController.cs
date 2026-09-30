using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Data;
using SALVADOR_API.Dtos;
using SALVADOR_API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SALVADOR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalidasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetSalidas()
        {
            var salidas = await _context.Salidas
                .OrderBy(s => s.Fecha)
                .ThenBy(s => s.Hora)
                .Select(s => new SalidaResumenDto
                {
                    ID_Salida = s.ID_Salida,
                    Placa_Vehiculo = s.Placa_Vehiculo,
                    Origen = s.Origen,
                    Destino = s.Destino,
                    Fecha = s.Fecha,
                    Hora = s.Hora,
                    Tarifa = s.Tarifa,
                    Asientos_Totales = s.Asientos.Count(),
                    Asientos_Vendidos = s.Asientos.Count(a => a.Venta != null)
                })
                .ToListAsync();

            return Ok(salidas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalidaDetalleDto>> GetSalida(int id)
        {
            var salida = await _context.Salidas
                .Include(s => s.Asientos)
                    .ThenInclude(a => a.Venta)
                .FirstOrDefaultAsync(s => s.ID_Salida == id);

            if (salida == null)
            {
                return NotFound("El viaje seleccionado no existe en la base de datos.");
            }

            var dto = new SalidaDetalleDto
            {
                ID_Salida = salida.ID_Salida,
                Placa_Vehiculo = salida.Placa_Vehiculo,
                Origen = salida.Origen,
                Destino = salida.Destino,
                Fecha = salida.Fecha,
                Hora = salida.Hora,
                Tarifa = salida.Tarifa,
                Asientos = salida.Asientos
                    .OrderBy(a => a.Numero)
                    .Select(a => new AsientoDto
                    {
                        ID_Asiento = a.ID_Asiento,
                        ID_Salida = a.ID_Salida,
                        Numero = a.Numero,
                        Vendido = a.Venta != null
                    })
                    .ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<SalidaResumenDto>> CrearSalida(SalidaDto request)
        {
            string? error = ValidarSalida(request);
            if (error != null)
            {
                return BadRequest(error);
            }

            if (request.Fecha.Date < DateTime.Today)
            {
                return BadRequest("No se puede programar una salida en una fecha pasada.");
            }

            var vehiculo = await _context.Vehiculos.FindAsync(request.Placa_Vehiculo);
            if (vehiculo == null)
            {
                return BadRequest("El bus (placa) indicado no existe.");
            }

            var salida = new Salida
            {
                Placa_Vehiculo = request.Placa_Vehiculo,
                Origen = request.Origen.Trim(),
                Destino = request.Destino.Trim(),
                Fecha = request.Fecha.Date,
                Hora = request.Hora,
                Tarifa = request.Tarifa
            };

            _context.Salidas.Add(salida);
            await _context.SaveChangesAsync();

            CrearAsientos(salida.ID_Salida, vehiculo.Capacidad);
            await _context.SaveChangesAsync();

            return Ok(new SalidaResumenDto
            {
                ID_Salida = salida.ID_Salida,
                Placa_Vehiculo = salida.Placa_Vehiculo,
                Origen = salida.Origen,
                Destino = salida.Destino,
                Fecha = salida.Fecha,
                Hora = salida.Hora,
                Tarifa = salida.Tarifa,
                Asientos_Totales = vehiculo.Capacidad,
                Asientos_Vendidos = 0
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SalidaResumenDto>> ActualizarSalida(int id, SalidaDto request)
        {
            string? error = ValidarSalida(request);
            if (error != null)
            {
                return BadRequest(error);
            }

            var salida = await _context.Salidas
                .Include(s => s.Asientos)
                    .ThenInclude(a => a.Venta)
                .FirstOrDefaultAsync(s => s.ID_Salida == id);

            if (salida == null)
            {
                return NotFound("El viaje que intentas editar no existe.");
            }

            var vehiculo = await _context.Vehiculos.FindAsync(request.Placa_Vehiculo);
            if (vehiculo == null)
            {
                return BadRequest("El bus (placa) indicado no existe.");
            }

            int vendidos = salida.Asientos.Count(a => a.Venta != null);

            if (vehiculo.Capacidad != salida.Asientos.Count)
            {
                if (vendidos > 0)
                {
                    return BadRequest("No se puede cambiar a un bus con distinta capacidad porque esta salida ya tiene pasajes vendidos.");
                }

                _context.Asientos.RemoveRange(salida.Asientos);
                CrearAsientos(salida.ID_Salida, vehiculo.Capacidad);
            }

            salida.Placa_Vehiculo = request.Placa_Vehiculo;
            salida.Origen = request.Origen.Trim();
            salida.Destino = request.Destino.Trim();
            salida.Fecha = request.Fecha.Date;
            salida.Hora = request.Hora;
            salida.Tarifa = request.Tarifa;

            await _context.SaveChangesAsync();

            return Ok(new SalidaResumenDto
            {
                ID_Salida = salida.ID_Salida,
                Placa_Vehiculo = salida.Placa_Vehiculo,
                Origen = salida.Origen,
                Destino = salida.Destino,
                Fecha = salida.Fecha,
                Hora = salida.Hora,
                Tarifa = salida.Tarifa,
                Asientos_Totales = vehiculo.Capacidad,
                Asientos_Vendidos = vendidos
            });
        }

        private static string? ValidarSalida(SalidaDto request)
        {
            if (string.Equals(request.Origen.Trim(), request.Destino.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return "El origen y el destino no pueden ser iguales.";
            }

            if (request.Tarifa <= 0)
            {
                return "La tarifa debe ser mayor a 0.";
            }

            return null;
        }

        private void CrearAsientos(int idSalida, int capacidad)
        {
            for (int numero = 1; numero <= capacidad; numero++)
            {
                _context.Asientos.Add(new Asiento
                {
                    ID_Salida = idSalida,
                    Numero = numero,
                    Estado = "Libre"
                });
            }
        }
    }
}
