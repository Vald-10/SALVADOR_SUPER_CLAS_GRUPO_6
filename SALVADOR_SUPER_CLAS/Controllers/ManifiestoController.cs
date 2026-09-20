using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_SUPER_CLAS.Data;
using SALVADOR_SUPER_CLAS.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SALVADOR_SUPER_CLAS.Controllers
{
    public class ManifiestoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManifiestoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> RevisionManifiesto(int idSalida = 1)
        {
            var salida = await _context.Salidas
                .Include(s => s.Asientos)
                    .ThenInclude(a => a.Venta)
                        .ThenInclude(v => v.Pasajero)
                .FirstOrDefaultAsync(s => s.ID_Salida == idSalida);

            if (salida == null)
            {
                return NotFound("El viaje solicitado no existe.");
            }

            var viewModel = new ManifiestoViewModel
            {
                ID_Salida = salida.ID_Salida,
                Placa_Vehiculo = salida.Placa_Vehiculo,
                Origen = salida.Origen,
                Destino = salida.Destino,
                Fecha = salida.Fecha,
                Hora = salida.Hora,

                Pasajeros = salida.Asientos
                    .Where(a => a.Venta != null && a.Venta.Pasajero != null)
                    .OrderBy(a => a.Numero)
                    .Select(a => new PasajeroManifiesto
                    {
                        NumeroAsiento = a.Numero,
                        Documento = a.Venta.Pasajero.Documento,
                        Nombre_Completo = a.Venta.Pasajero.Nombre_Completo,
                        Nacionalidad = a.Venta.Pasajero.Nacionalidad,
                        Genero = a.Venta.Pasajero.Genero
                    }).ToList()
            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> GenerarManifiestoPdf(int idSalida = 1) 
        {
            var salida = await _context.Salidas
                .Include(s => s.Asientos)
                    .ThenInclude(a => a.Venta)
                        .ThenInclude(v => v.Pasajero)
                .FirstOrDefaultAsync(s => s.ID_Salida == idSalida);

            if (salida == null) return NotFound("El viaje solicitado no existe.");

            var viewModel = new ManifiestoViewModel
            {
                ID_Salida = salida.ID_Salida,
                Placa_Vehiculo = salida.Placa_Vehiculo,
                Origen = salida.Origen,
                Destino = salida.Destino,
                Fecha = salida.Fecha,
                Hora = salida.Hora,
                Pasajeros = salida.Asientos
                    .Where(a => a.Venta != null && a.Venta.Pasajero != null)
                    .OrderBy(a => a.Numero)
                    .Select(a => new PasajeroManifiesto
                    {
                        NumeroAsiento = a.Numero,
                        Documento = a.Venta.Pasajero.Documento,
                        Nombre_Completo = a.Venta.Pasajero.Nombre_Completo,
                        Nacionalidad = a.Venta.Pasajero.Nacionalidad,
                        Genero = a.Venta.Pasajero.Genero
                    }).ToList()
            };

            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("MANIFIESTO OFICIAL DE PASAJEROS").Bold().FontSize(16);
                            col.Item().Text($"Ruta: {viewModel.Origen} - {viewModel.Destino}");
                            col.Item().Text($"Fecha: {viewModel.Fecha:dd/MM/yyyy} | Hora: {viewModel.Hora:hh\\:mm}");
                            col.Item().Text($"Bus Placa: {viewModel.Placa_Vehiculo}").Bold();
                        });
                    });

                    page.Content().PaddingVertical(1, Unit.Centimetre).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(70);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(2).Text("Asiento").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(2).Text("Nombre Completo").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(2).Text("CI / Pasaporte").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(2).Text("Nacionalidad").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(2).Text("Género").Bold();
                        });

                        foreach (var pasajero in viewModel.Pasajeros)
                        {
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(2).Text(pasajero.NumeroAsiento.ToString());
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(2).Text(pasajero.Nombre_Completo);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(2).Text(pasajero.Documento);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(2).Text(pasajero.Nacionalidad);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(2).Text(pasajero.Genero);
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            });

            byte[] pdfBytes = document.GeneratePdf();
            string fileName = $"Manifiesto_{viewModel.Placa_Vehiculo}_{viewModel.Fecha:yyyyMMdd}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}