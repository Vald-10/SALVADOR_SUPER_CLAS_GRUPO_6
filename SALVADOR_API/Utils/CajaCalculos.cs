using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Data;
using SALVADOR_API.Dtos;
using SALVADOR_API.Models;
using System.Linq;

namespace SALVADOR_API.Utils
{
    public static class CajaCalculos
    {
        public static IQueryable<Caja> CajasConDetalle(ApplicationDbContext context)
        {
            return context.Cajas
                .Include(c => c.Usuario)
                .Include(c => c.Ventas)
                    .ThenInclude(v => v.Asiento)
                        .ThenInclude(a => a.Salida)
                .Include(c => c.Ventas)
                    .ThenInclude(v => v.Pasajero);
        }

        public static CierreCajaDto ConstruirArqueo(Caja caja, bool incluirVentas)
        {
            decimal totalEfectivo = caja.Ventas.Where(v => v.Metodo_Pago == "Efectivo").Sum(v => v.Monto);
            decimal totalQR = caja.Ventas.Where(v => v.Metodo_Pago == "QR").Sum(v => v.Monto);
            decimal totalVentas = caja.Ventas.Sum(v => v.Monto);

            var dto = new CierreCajaDto
            {
                ID_Caja = caja.ID_Caja,
                ID_Usuario = caja.ID_Usuario,
                NombreUsuario = caja.Usuario.NombreUsuario,
                NombreCompleto = caja.Usuario.NombreCompleto,
                Fecha_Apertura = caja.Fecha_Apertura,
                Fecha_Cierre = caja.Fecha_Cierre,
                Estado = caja.Estado,
                Monto_Inicial = caja.Monto_Inicial,
                Cantidad_Boletos = caja.Ventas.Count,
                Total_Efectivo = totalEfectivo,
                Total_QR = totalQR,
                Total_Ventas = totalVentas,
                Monto_Final = caja.Monto_Inicial + totalVentas
            };

            if (incluirVentas)
            {
                dto.Ventas = caja.Ventas
                    .OrderBy(v => v.Fecha_Transaccion)
                    .Select(v => new VentaCajaDto
                    {
                        ID_Venta = v.ID_Venta,
                        Fecha_Transaccion = v.Fecha_Transaccion,
                        Numero_Asiento = v.Asiento.Numero,
                        Ruta = v.Asiento.Salida.Origen + " - " + v.Asiento.Salida.Destino,
                        Pasajero = v.Pasajero.Nombre_Completo,
                        Metodo_Pago = v.Metodo_Pago,
                        Monto = v.Monto
                    })
                    .ToList();
            }

            return dto;
        }

        public static CajaDto ConstruirCaja(Caja caja)
        {
            return new CajaDto
            {
                ID_Caja = caja.ID_Caja,
                ID_Usuario = caja.ID_Usuario,
                NombreUsuario = caja.Usuario.NombreUsuario,
                NombreCompleto = caja.Usuario.NombreCompleto,
                Fecha_Apertura = caja.Fecha_Apertura,
                Fecha_Cierre = caja.Fecha_Cierre,
                Monto_Inicial = caja.Monto_Inicial,
                Estado = caja.Estado
            };
        }
    }
}
