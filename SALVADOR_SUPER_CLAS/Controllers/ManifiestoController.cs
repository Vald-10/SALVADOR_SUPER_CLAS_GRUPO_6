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
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SALVADOR_SUPER_CLAS.Controllers
{
    [Authorize(Roles = Roles.Administrador)]
    public class ManifiestoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ManifiestoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SalvadorApi");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var salidas = await _httpClient.GetFromJsonAsync<List<SalidaResumenDto>>("api/Salidas");
            return View(salidas ?? new List<SalidaResumenDto>());
        }

        [HttpGet]
        public async Task<IActionResult> RevisionManifiesto(int idSalida)
        {
            var viewModel = await ObtenerManifiestoAsync(idSalida);
            if (viewModel == null)
            {
                TempData["Error"] = "El viaje solicitado no existe.";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GenerarManifiestoPdf(int idSalida)
        {
            var viewModel = await ObtenerManifiestoAsync(idSalida);
            if (viewModel == null)
            {
                TempData["Error"] = "El viaje solicitado no existe.";
                return RedirectToAction("Index");
            }

            string generadoPor = User.FindFirst("NombreCompleto")?.Value ?? User.Identity?.Name ?? "";

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(header =>
                    {
                        header.Item().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("SALVADOR SUPER CLASS").Bold().FontSize(18).FontColor("#1E293B");
                                col.Item().Text("MANIFIESTO OFICIAL DE PASAJEROS - ADUANA").SemiBold().FontSize(12).FontColor("#548383");
                            });
                            row.ConstantItem(220).AlignRight().Column(col =>
                            {
                                col.Item().AlignRight().Text($"Salida N° {viewModel.ID_Salida}").SemiBold();
                                col.Item().AlignRight().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9);
                                col.Item().AlignRight().Text($"Por: {generadoPor}").FontSize(9);
                            });
                        });

                        header.Item().PaddingTop(8).Background("#F8FAFC").Border(1).BorderColor(Colors.Grey.Lighten2).Padding(8).Row(row =>
                        {
                            row.RelativeItem().Text(t => { t.Span("Ruta: ").SemiBold(); t.Span($"{viewModel.Origen} - {viewModel.Destino}"); });
                            row.RelativeItem().Text(t => { t.Span("Fecha: ").SemiBold(); t.Span($"{viewModel.Fecha:dd/MM/yyyy}"); });
                            row.RelativeItem().Text(t => { t.Span("Hora: ").SemiBold(); t.Span(viewModel.Hora.ToString(@"hh\:mm")); });
                            row.RelativeItem().Text(t => { t.Span("Bus (placa): ").SemiBold(); t.Span(viewModel.Placa_Vehiculo); });
                            row.RelativeItem().Text(t => { t.Span("Pasajeros: ").SemiBold(); t.Span($"{viewModel.Pasajeros.Count} de {viewModel.Capacidad}"); });
                        });
                    });

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.ConstantColumn(55);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(1.3f);
                        });

                        table.Header(header =>
                        {
                            foreach (var titulo in new[] { "N°", "Asiento", "Nombre Completo", "Tipo Doc.", "N° Documento", "Nacionalidad", "Género" })
                            {
                                header.Cell().Background("#1E293B").Padding(4).Text(titulo).Bold().FontColor(Colors.White);
                            }
                        });

                        int n = 1;
                        foreach (var pasajero in viewModel.Pasajeros)
                        {
                            string fondo = n % 2 == 0 ? "#F8FAFC" : "#FFFFFF";
                            table.Cell().Background(fondo).BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(n.ToString());
                            table.Cell().Background(fondo).BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(pasajero.NumeroAsiento.ToString()).SemiBold();
                            table.Cell().Background(fondo).BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(pasajero.Nombre_Completo);
                            table.Cell().Background(fondo).BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(pasajero.Tipo_Documento);
                            table.Cell().Background(fondo).BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(pasajero.Documento);
                            table.Cell().Background(fondo).BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(pasajero.Nacionalidad);
                            table.Cell().Background(fondo).BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(pasajero.Genero);
                            n++;
                        }

                        if (viewModel.Pasajeros.Count == 0)
                        {
                            table.Cell().ColumnSpan(7).Padding(10).AlignCenter().Text("No hay pasajeros registrados para este viaje.").Italic();
                        }
                    });

                    page.Footer().Column(footer =>
                    {
                        footer.Item().PaddingTop(25).Row(row =>
                        {
                            row.RelativeItem().AlignCenter().Column(c =>
                            {
                                c.Item().Width(180).LineHorizontal(1);
                                c.Item().AlignCenter().Text("Firma Administrador").FontSize(9);
                            });
                            row.RelativeItem().AlignCenter().Column(c =>
                            {
                                c.Item().Width(180).LineHorizontal(1);
                                c.Item().AlignCenter().Text("Firma Chofer / Tripulación").FontSize(9);
                            });
                            row.RelativeItem().AlignCenter().Column(c =>
                            {
                                c.Item().Width(180).LineHorizontal(1);
                                c.Item().AlignCenter().Text("Sello Control Aduanero").FontSize(9);
                            });
                        });

                        footer.Item().PaddingTop(6).AlignCenter().Text(x =>
                        {
                            x.Span("Página ").FontSize(8);
                            x.CurrentPageNumber().FontSize(8);
                            x.Span(" de ").FontSize(8);
                            x.TotalPages().FontSize(8);
                        });
                    });
                });
            });

            byte[] pdfBytes = document.GeneratePdf();
            string fileName = $"Manifiesto_{viewModel.Placa_Vehiculo}_{viewModel.Fecha:yyyyMMdd}.pdf";

            Response.Headers["Content-Disposition"] = $"inline; filename={fileName}";
            return File(pdfBytes, "application/pdf");
        }

        private async Task<ManifiestoViewModel?> ObtenerManifiestoAsync(int idSalida)
        {
            var response = await _httpClient.GetAsync($"api/Manifiesto/{idSalida}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ManifiestoViewModel>();
        }
    }
}
