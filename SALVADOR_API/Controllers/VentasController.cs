using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Data;
using SALVADOR_API.Dtos;
using SALVADOR_API.Models;
using System;
using System.Threading.Tasks;

namespace SALVADOR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<VentaResultDto>> Crear(VentaRequestDto request)
        {
            if (request.Metodo_Pago != "Efectivo" && request.Metodo_Pago != "QR")
            {
                return Ok(new VentaResultDto { Success = false, Message = "El método de pago debe ser Efectivo o QR." });
            }

            if (request.Tipo_Documento != "CI" && request.Tipo_Documento != "RUT" && request.Tipo_Documento != "Pasaporte")
            {
                return Ok(new VentaResultDto { Success = false, Message = "El tipo de documento debe ser CI, RUT o Pasaporte." });
            }

            var usuario = await _context.Usuarios.FindAsync(request.ID_Usuario);
            if (usuario == null)
            {
                return Ok(new VentaResultDto { Success = false, Message = "El boletero no existe." });
            }

            DateTime hoy = DateTime.Today;
            var caja = await _context.Cajas.FirstOrDefaultAsync(c => c.ID_Usuario == request.ID_Usuario
                                                                  && c.Estado == "Abierta"
                                                                  && c.Fecha_Apertura >= hoy
                                                                  && c.Fecha_Apertura < hoy.AddDays(1));
            if (caja == null)
            {
                return Ok(new VentaResultDto { Success = false, Message = "Debes abrir la caja del día antes de vender pasajes." });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var asiento = await _context.Asientos
                    .Include(a => a.Salida)
                    .Include(a => a.Venta)
                    .FirstOrDefaultAsync(a => a.ID_Asiento == request.ID_Asiento);

                if (asiento == null || asiento.Venta != null)
                {
                    return Ok(new VentaResultDto { Success = false, Message = "El asiento ya fue vendido o no está disponible." });
                }

                if (asiento.Salida.Fecha < hoy)
                {
                    return Ok(new VentaResultDto { Success = false, Message = "Este viaje ya salió; no se pueden vender más pasajes." });
                }

                var pasajero = await _context.Pasajeros.FindAsync(request.Documento);
                if (pasajero == null)
                {
                    pasajero = new Pasajero
                    {
                        Documento = request.Documento,
                        Tipo_Documento = request.Tipo_Documento,
                        Nombre_Completo = request.Nombre_Completo,
                        Nacionalidad = request.Nacionalidad,
                        Genero = request.Genero
                    };
                    _context.Pasajeros.Add(pasajero);
                }
                else
                {
                    pasajero.Tipo_Documento = request.Tipo_Documento;
                    pasajero.Nombre_Completo = request.Nombre_Completo;
                    pasajero.Nacionalidad = request.Nacionalidad;
                    pasajero.Genero = request.Genero;
                }
                await _context.SaveChangesAsync();

                var nuevaVenta = new Venta
                {
                    ID_Asiento = request.ID_Asiento,
                    Documento_Pasajero = request.Documento,
                    Fecha_Transaccion = DateTime.Now,
                    Monto = asiento.Salida.Tarifa,
                    Metodo_Pago = request.Metodo_Pago,
                    Token_Boletero = usuario.NombreUsuario,
                    ID_Caja = caja.ID_Caja
                };
                _context.Ventas.Add(nuevaVenta);

                asiento.Estado = "Vendido";

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new VentaResultDto { Success = true, Message = "Venta registrada exitosamente.", ID_Venta = nuevaVenta.ID_Venta });
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync();
                return Ok(new VentaResultDto { Success = false, Message = "El asiento fue vendido a otra persona en este instante. Elige otro." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDetalleDto>> GetVenta(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Pasajero)
                .Include(v => v.Asiento)
                    .ThenInclude(a => a.Salida)
                .FirstOrDefaultAsync(v => v.ID_Venta == id);

            if (venta == null)
                return NotFound("No se encontró el boleto solicitado.");

            var dto = new VentaDetalleDto
            {
                ID_Venta = venta.ID_Venta,
                ID_Caja = venta.ID_Caja,
                ID_Salida = venta.Asiento.ID_Salida,
                Fecha_Transaccion = venta.Fecha_Transaccion,
                Tipo_Documento = venta.Pasajero.Tipo_Documento,
                Documento_Pasajero = venta.Documento_Pasajero,
                Nombre_Completo = venta.Pasajero.Nombre_Completo,
                Nacionalidad = venta.Pasajero.Nacionalidad,
                Monto = venta.Monto,
                Metodo_Pago = venta.Metodo_Pago,
                Token_Boletero = venta.Token_Boletero,
                Numero_Asiento = venta.Asiento.Numero,
                Placa_Vehiculo = venta.Asiento.Salida.Placa_Vehiculo,
                Origen = venta.Asiento.Salida.Origen,
                Destino = venta.Asiento.Salida.Destino,
                Fecha = venta.Asiento.Salida.Fecha,
                Hora = venta.Asiento.Salida.Hora
            };

            return Ok(dto);
        }
    }
}
