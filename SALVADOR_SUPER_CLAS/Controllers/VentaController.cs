using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SALVADOR_SUPER_CLAS.ApiModels;
using SALVADOR_SUPER_CLAS.Utils;
using SALVADOR_SUPER_CLAS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SALVADOR_SUPER_CLAS.Controllers
{
    [Authorize(Roles = Roles.Boletero)]
    public class VentaController : Controller
    {
        private readonly HttpClient _httpClient;

        public VentaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SalvadorApi");
        }

        private async Task<CajaDto?> ObtenerCajaDeHoyAsync()
        {
            int idUsuario = ApiHelper.ObtenerIdUsuario(User);
            var response = await _httpClient.GetAsync($"api/Cajas/actual/{idUsuario}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<CajaDto>();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var caja = await ObtenerCajaDeHoyAsync();
            if (caja == null || !caja.EstaAbierta)
            {
                return View("CajaRequerida", caja);
            }

            var salidas = await _httpClient.GetFromJsonAsync<List<SalidaResumenDto>>("api/Salidas") ?? new List<SalidaResumenDto>();

            var disponibles = salidas
                .Where(s => s.Fecha.Date >= DateTime.Today)
                .OrderBy(s => s.Fecha).ThenBy(s => s.Hora)
                .ToList();

            return View(disponibles);
        }

        [HttpGet]
        public async Task<IActionResult> SeleccionarAsiento(int idSalida)
        {
            var caja = await ObtenerCajaDeHoyAsync();
            if (caja == null || !caja.EstaAbierta)
            {
                return View("CajaRequerida", caja);
            }

            var response = await _httpClient.GetAsync($"api/Salidas/{idSalida}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "El viaje seleccionado no existe.";
                return RedirectToAction("Index");
            }

            var salida = await response.Content.ReadFromJsonAsync<SalidaDetalleDto>();
            if (salida!.Fecha.Date < DateTime.Today)
            {
                TempData["Error"] = "Ese viaje ya salió; elige otro.";
                return RedirectToAction("Index");
            }

            return View(salida);
        }

        [HttpGet]
        public async Task<IActionResult> EstadoAsientos(int idSalida)
        {
            var response = await _httpClient.GetAsync($"api/Salidas/{idSalida}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var salida = await response.Content.ReadFromJsonAsync<SalidaDetalleDto>();
            var estado = salida!.Asientos.Select(a => new { id = a.ID_Asiento, vendido = a.Vendido });
            return Json(estado);
        }

        [HttpGet]
        public async Task<IActionResult> FormularioVenta(int idAsiento)
        {
            var caja = await ObtenerCajaDeHoyAsync();
            if (caja == null || !caja.EstaAbierta)
            {
                return View("CajaRequerida", caja);
            }

            var response = await _httpClient.GetAsync($"api/Asientos/{idAsiento}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "El asiento seleccionado no existe.";
                return RedirectToAction("Index");
            }

            var asiento = await response.Content.ReadFromJsonAsync<AsientoDto>();
            if (asiento!.Vendido)
            {
                TempData["Error"] = $"El asiento N° {asiento.Numero} ya fue vendido. Elige otro.";
                return RedirectToAction("SeleccionarAsiento", new { idSalida = asiento.ID_Salida });
            }

            var model = new VentaPresencialViewModel
            {
                ID_Asiento = asiento.ID_Asiento,
                ID_Salida = asiento.ID_Salida
            };
            await CargarResumenViajeAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarVentaPresencial(VentaPresencialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await CargarResumenViajeAsync(model);
                return View("FormularioVenta", model);
            }

            var response = await _httpClient.PostAsJsonAsync("api/Ventas", new VentaRequestDto
            {
                ID_Asiento = model.ID_Asiento,
                ID_Usuario = ApiHelper.ObtenerIdUsuario(User),
                Tipo_Documento = model.Tipo_Documento,
                Documento = model.Documento.Trim(),
                Nombre_Completo = model.Nombre_Completo.Trim(),
                Nacionalidad = model.Nacionalidad,
                Genero = model.Genero,
                Metodo_Pago = model.Metodo_Pago
            });

            VentaResultDto? resultado = response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<VentaResultDto>()
                : null;

            if (resultado == null || !resultado.Success)
            {
                ModelState.AddModelError("", resultado?.Message ?? "No se pudo registrar la venta. Revisa los datos.");
                await CargarResumenViajeAsync(model);
                return View("FormularioVenta", model);
            }

            return RedirectToAction("VentaExitosa", new { idVenta = resultado.ID_Venta });
        }

        [HttpGet]
        public async Task<IActionResult> VentaExitosa(int idVenta)
        {
            var response = await _httpClient.GetAsync($"api/Ventas/{idVenta}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var venta = await response.Content.ReadFromJsonAsync<VentaDetalleDto>();
            return View(venta);
        }

        private async Task CargarResumenViajeAsync(VentaPresencialViewModel model)
        {
            var salida = await _httpClient.GetFromJsonAsync<SalidaDetalleDto>($"api/Salidas/{model.ID_Salida}");
            if (salida == null) return;

            model.Origen = salida.Origen;
            model.Destino = salida.Destino;
            model.Fecha = salida.Fecha;
            model.Hora = salida.Hora;
            model.Tarifa = salida.Tarifa;
            model.Placa_Vehiculo = salida.Placa_Vehiculo;
            model.NumeroAsiento = salida.Asientos.FirstOrDefault(a => a.ID_Asiento == model.ID_Asiento)?.Numero ?? 0;
        }

        [HttpGet]
        public async Task<IActionResult> GenerarBoletoPdf(int idVenta)
        {
            var response = await _httpClient.GetAsync($"api/Ventas/{idVenta}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("No se encontró el boleto solicitado.");
            }

            var venta = await response.Content.ReadFromJsonAsync<VentaDetalleDto>();

            if (venta == null) return NotFound("No se encontró el boleto solicitado.");

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));
                    page.Header().Column(col =>
                    {
                        col.Item().AlignCenter().Text("SALVADOR SUPER CLASS")
                           .SemiBold().FontSize(20).FontColor("#1E293B");
                        col.Item().AlignCenter().Text("Boleto de Viaje Oficial").Underline();
                        col.Item().AlignCenter().Text($"N° de venta: {venta.ID_Venta:D6}").FontSize(9).FontColor(Colors.Grey.Darken1);
                        col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    });

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Spacing(5);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(100);
                                columns.RelativeColumn();
                            });

                            table.Cell().Text("Pasajero:").SemiBold();
                            table.Cell().Text(venta.Nombre_Completo);

                            table.Cell().Text($"{venta.Tipo_Documento}:").SemiBold();
                            table.Cell().Text(venta.Documento_Pasajero);

                            table.Cell().Text("Nacionalidad:").SemiBold();
                            table.Cell().Text(venta.Nacionalidad);

                            table.Cell().ColumnSpan(2).PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten4);

                            table.Cell().Text("Ruta:").SemiBold();
                            table.Cell().Text($"{venta.Origen} a {venta.Destino}").SemiBold();

                            table.Cell().Text("Fecha y Hora:").SemiBold();
                            table.Cell().Text($"{venta.Fecha:dd/MM/yyyy} - {venta.Hora.ToString(@"hh\:mm")}");

                            table.Cell().Text("Bus (placa):").SemiBold();
                            table.Cell().Text(venta.Placa_Vehiculo);

                            table.Cell().ColumnSpan(2).PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten4);

                            table.Cell().Text("Tarifa:").SemiBold();
                            table.Cell().Text(ApiHelper.Bs(venta.Monto));

                            table.Cell().Text("Pago:").SemiBold();
                            table.Cell().Text(venta.Metodo_Pago);
                        });

                        col.Spacing(15);

                        col.Item().AlignCenter().Background("#548383").Padding(10).Text($"ASIENTO N° {venta.Numero_Asiento}")
                           .FontSize(18).SemiBold().FontColor(Colors.White);
                    });

                    page.Footer().AlignCenter().Column(col =>
                    {
                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        col.Item().PaddingTop(5).Text($"Emitido por: {venta.Token_Boletero} | Caja N° {venta.ID_Caja} | {venta.Fecha_Transaccion:dd/MM/yyyy HH:mm}").FontSize(8).FontColor(Colors.Grey.Medium);
                        col.Item().Text("Este boleto es personal e intransferible. Presentar documento de identidad al abordar.").FontSize(8).FontColor(Colors.Grey.Medium);
                    });
                });
            });

            byte[] pdfBytes = document.GeneratePdf();

            Response.Headers["Content-Disposition"] = $"inline; filename=Boleto_{venta.ID_Venta}_Asiento{venta.Numero_Asiento}.pdf";
            return File(pdfBytes, "application/pdf");
        }
    }
}
