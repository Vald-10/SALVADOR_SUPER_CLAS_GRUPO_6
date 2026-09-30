using System;
using System.Collections.Generic;

namespace SALVADOR_SUPER_CLAS.ApiModels
{
    public class AsientoDto
    {
        public int ID_Asiento { get; set; }
        public int ID_Salida { get; set; }
        public int Numero { get; set; }
        public bool Vendido { get; set; }
    }

    public class SalidaDetalleDto
    {
        public int ID_Salida { get; set; }
        public string Placa_Vehiculo { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Tarifa { get; set; }
        public List<AsientoDto> Asientos { get; set; } = new List<AsientoDto>();
    }

    public class SalidaResumenDto
    {
        public int ID_Salida { get; set; }
        public string Placa_Vehiculo { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Tarifa { get; set; }
        public int Asientos_Totales { get; set; }
        public int Asientos_Vendidos { get; set; }

        public int Asientos_Libres => Asientos_Totales - Asientos_Vendidos;
    }

    public class SalidaDto
    {
        public string Placa_Vehiculo { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Tarifa { get; set; }
    }

    public class VehiculoDto
    {
        public string Placa { get; set; } = null!;
        public int Capacidad { get; set; }
    }

    public class UsuarioLoginResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public int? ID_Usuario { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Rol { get; set; }
    }

    public class AperturaCajaDto
    {
        public int ID_Usuario { get; set; }
        public DateTime Fecha_Apertura { get; set; }
        public decimal Monto_Inicial { get; set; }
    }

    public class CerrarCajaRequestDto
    {
        public int ID_Usuario { get; set; }
    }

    public class CajaDto
    {
        public int ID_Caja { get; set; }
        public int ID_Usuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public DateTime Fecha_Apertura { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        public decimal Monto_Inicial { get; set; }
        public string Estado { get; set; } = null!;

        public bool EstaAbierta => Estado == "Abierta";
    }

    public class CierreCajaDto
    {
        public int ID_Caja { get; set; }
        public int ID_Usuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public DateTime Fecha_Apertura { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        public string Estado { get; set; } = null!;

        public decimal Monto_Inicial { get; set; }
        public int Cantidad_Boletos { get; set; }
        public decimal Total_Efectivo { get; set; }
        public decimal Total_QR { get; set; }
        public decimal Total_Ventas { get; set; }
        public decimal Monto_Final { get; set; }

        public List<VentaCajaDto> Ventas { get; set; } = new List<VentaCajaDto>();

        public bool EstaAbierta => Estado == "Abierta";
    }

    public class VentaCajaDto
    {
        public int ID_Venta { get; set; }
        public DateTime Fecha_Transaccion { get; set; }
        public int Numero_Asiento { get; set; }
        public string Ruta { get; set; } = null!;
        public string Pasajero { get; set; } = null!;
        public string Metodo_Pago { get; set; } = null!;
        public decimal Monto { get; set; }
    }

    public class VentaRequestDto
    {
        public int ID_Asiento { get; set; }
        public int ID_Usuario { get; set; }
        public string Tipo_Documento { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Nombre_Completo { get; set; } = null!;
        public string Nacionalidad { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public string Metodo_Pago { get; set; } = null!;
    }

    public class VentaResultDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int? ID_Venta { get; set; }
    }

    public class VentaDetalleDto
    {
        public int ID_Venta { get; set; }
        public int? ID_Caja { get; set; }
        public int ID_Salida { get; set; }
        public DateTime Fecha_Transaccion { get; set; }
        public string Tipo_Documento { get; set; } = null!;
        public string Documento_Pasajero { get; set; } = null!;
        public string Nombre_Completo { get; set; } = null!;
        public string Nacionalidad { get; set; } = null!;
        public decimal Monto { get; set; }
        public string Metodo_Pago { get; set; } = null!;
        public string Token_Boletero { get; set; } = null!;
        public int Numero_Asiento { get; set; }
        public string Placa_Vehiculo { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
    }

    public class IngresoDiaDto
    {
        public DateTime Fecha { get; set; }
        public int Boletos { get; set; }
        public decimal Total { get; set; }
    }

    public class OcupacionSalidaDto
    {
        public int ID_Salida { get; set; }
        public string Ruta { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Placa_Vehiculo { get; set; } = null!;
        public int Vendidos { get; set; }
        public int Capacidad { get; set; }
        public decimal Ingresos { get; set; }

        public int Porcentaje => Capacidad == 0 ? 0 : (int)Math.Round(Vendidos * 100.0 / Capacidad);
    }

    public class ReporteDto
    {
        public decimal Ingresos_Totales { get; set; }
        public int Boletos_Vendidos { get; set; }
        public int Salidas_Programadas { get; set; }
        public int Cajas_Abiertas { get; set; }
        public decimal Total_Efectivo { get; set; }
        public decimal Total_QR { get; set; }
        public List<IngresoDiaDto> Ingresos_Ultimos_Dias { get; set; } = new List<IngresoDiaDto>();
        public List<OcupacionSalidaDto> Ocupacion { get; set; } = new List<OcupacionSalidaDto>();
        public List<CierreCajaDto> Historial_Cajas { get; set; } = new List<CierreCajaDto>();
    }
}
