using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SALVADOR_API.Dtos
{
    public class AsientoDto
    {
        public int ID_Asiento { get; set; }
        public int ID_Salida { get; set; }
        public int Numero { get; set; }
        public bool Vendido { get; set; }
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

    public class SalidaDto
    {
        [Required]
        [MaxLength(15)]
        public string Placa_Vehiculo { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Origen { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Destino { get; set; } = null!;

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }

        [Range(0.01, 100000)]
        public decimal Tarifa { get; set; }
    }

    public class VehiculoDto
    {
        public string Placa { get; set; } = null!;
        public int Capacidad { get; set; }
    }

    public class UsuarioLoginRequestDto
    {
        [Required]
        public string NombreUsuario { get; set; } = null!;

        [Required]
        public string Contrasena { get; set; } = null!;
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
        [Required]
        public int ID_Usuario { get; set; }

        [Required]
        public DateTime Fecha_Apertura { get; set; }

        [Range(0, 100000, ErrorMessage = "El monto inicial debe estar entre 0 y 100000.")]
        public decimal Monto_Inicial { get; set; }
    }

    public class CerrarCajaRequestDto
    {
        [Required]
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
        [Required]
        public int ID_Asiento { get; set; }

        [Required]
        public int ID_Usuario { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        [MaxLength(20)]
        public string Tipo_Documento { get; set; } = null!;

        [Required(ErrorMessage = "El Documento (CI/RUT/Pasaporte) es obligatorio.")]
        [MaxLength(30)]
        public string Documento { get; set; } = null!;

        [Required(ErrorMessage = "El Nombre Completo es obligatorio.")]
        [MaxLength(150)]
        public string Nombre_Completo { get; set; } = null!;

        [Required(ErrorMessage = "La Nacionalidad es requerida por migración.")]
        [MaxLength(50)]
        public string Nacionalidad { get; set; } = null!;

        [Required(ErrorMessage = "El Género es obligatorio.")]
        [MaxLength(20)]
        public string Genero { get; set; } = null!;

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [MaxLength(20)]
        public string Metodo_Pago { get; set; } = null!;
    }

    public class VentaResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
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

    public class PasajeroManifiestoDto
    {
        public int NumeroAsiento { get; set; }
        public string Tipo_Documento { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Nombre_Completo { get; set; } = null!;
        public string Nacionalidad { get; set; } = null!;
        public string Genero { get; set; } = null!;
    }

    public class ManifiestoDto
    {
        public int ID_Salida { get; set; }
        public string Placa_Vehiculo { get; set; } = null!;
        public int Capacidad { get; set; }
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public List<PasajeroManifiestoDto> Pasajeros { get; set; } = new List<PasajeroManifiestoDto>();
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
