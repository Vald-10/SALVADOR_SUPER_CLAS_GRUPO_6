using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SALVADOR_SUPER_CLAS.Data;
using SALVADOR_SUPER_CLAS.Models;
using SALVADOR_SUPER_CLAS.ViewModels;
using System;
using System.Threading.Tasks;

namespace SALVADOR_SUPER_CLAS.Controllers
{
    public class VentaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> SeleccionarAsiento(int idSalida)
        {
            var salida = await _context.Salidas
                .Include(s => s.Asientos)
                    .ThenInclude(a => a.Venta)
                .FirstOrDefaultAsync(s => s.ID_Salida == idSalida);

            if (salida == null)
            {
                return NotFound("El viaje seleccionado no existe en la base de datos.");
            }

            return View(salida);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarReserva(int idAsiento, string documentoPasajero, string nombreCompleto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var asiento = await _context.Asientos
                    .Include(a => a.Venta)
                    .Include(a => a.Salida)
                    .FirstOrDefaultAsync(a => a.ID_Asiento == idAsiento);

                if (asiento == null)
                    return Json(new { success = false, message = "Error: Asiento no encontrado." });

                if (asiento.Venta != null)
                {
                    return Json(new { success = false, message = "El asiento ya fue reservado por otro usuario." });
                }

                var pasajero = await _context.Pasajeros.FindAsync(documentoPasajero);
                if (pasajero == null)
                {
                    pasajero = new Pasajero
                    {
                        Documento = documentoPasajero,
                        Nombre_Completo = nombreCompleto,
                        Nacionalidad = "No especificada",
                        Genero = "No especificado"
                    };
                    _context.Pasajeros.Add(pasajero);
                    await _context.SaveChangesAsync();
                }

                var nuevaVenta = new Venta
                {
                    ID_Asiento = idAsiento,
                    Documento_Pasajero = documentoPasajero,
                    Fecha_Transaccion = DateTime.Now,
                    Monto = asiento.Salida.Tarifa,
                    Metodo_Pago = "Efectivo",
                    Token_Boletero = Guid.NewGuid().ToString()
                };

                _context.Ventas.Add(nuevaVenta);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Json(new { success = true, message = "Reserva confirmada exitosamente." });
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = "Conflicto: El asiento fue comprado hace un instante. Elige otro." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = "Error interno del servidor.", detalle = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> FormularioVenta(int idAsiento)
        {
            var asiento = await _context.Asientos
                .Include(a => a.Venta)
                .FirstOrDefaultAsync(a => a.ID_Asiento == idAsiento);

            if (asiento == null || asiento.Venta != null)
            {
                return RedirectToAction("SeleccionarAsiento", new { idSalida = 1 });
            }

            var viewModel = new VentaPresencialViewModel
            {
                ID_Asiento = asiento.ID_Asiento,
                NumeroAsiento = asiento.Numero
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarVentaPresencial(VentaPresencialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("FormularioVenta", model);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var asiento = await _context.Asientos
                    .Include(a => a.Salida)
                    .FirstOrDefaultAsync(a => a.ID_Asiento == model.ID_Asiento);

                if (asiento == null || asiento.Estado == "Vendido")
                {
                    ModelState.AddModelError("", "El asiento ya fue vendido o no está disponible.");
                    return View("FormularioVenta", model);
                }

                var pasajero = await _context.Pasajeros.FindAsync(model.Documento);
                if (pasajero == null)
                {
                    pasajero = new Pasajero
                    {
                        Documento = model.Documento,
                        Nombre_Completo = model.Nombre_Completo,
                        Nacionalidad = model.Nacionalidad,
                        Genero = model.Genero
                    };
                    _context.Pasajeros.Add(pasajero);
                }
                else
                {
                    pasajero.Nombre_Completo = model.Nombre_Completo;
                    pasajero.Nacionalidad = model.Nacionalidad;
                    pasajero.Genero = model.Genero;
                    _context.Pasajeros.Update(pasajero);
                }
                await _context.SaveChangesAsync();

                var nuevaVenta = new Venta
                {
                    ID_Asiento = model.ID_Asiento,
                    Documento_Pasajero = model.Documento,
                    Fecha_Transaccion = DateTime.Now,
                    Monto = asiento.Salida.Tarifa,
                    Metodo_Pago = "Efectivo",
                    Token_Boletero = Guid.NewGuid().ToString()
                };
                _context.Ventas.Add(nuevaVenta);

                asiento.Estado = "Vendido";
                _context.Asientos.Update(asiento);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction("GenerarBoletoPdf", new { idVenta = nuevaVenta.ID_Venta });
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Error: El asiento fue vendido a otra persona en este instante.");
                return View("FormularioVenta", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GenerarBoletoPdf(int idVenta)
        {
            var venta = await _context.Ventas
                .Include(v => v.Pasajero)
                .Include(v => v.Asiento)
                    .ThenInclude(a => a.Salida)
                .FirstOrDefaultAsync(v => v.ID_Venta == idVenta);

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
                        col.Item().AlignCenter().Text("SALVADOR SUPER CLAS")
                           .SemiBold().FontSize(20).FontColor("#548383");
                        col.Item().AlignCenter().Text("Boleto de Viaje Oficial").Underline();
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
                            table.Cell().Text(venta.Pasajero.Nombre_Completo);

                            table.Cell().Text("CI/Pasaporte:").SemiBold();
                            table.Cell().Text(venta.Documento_Pasajero);

                            table.Cell().Text("Nacionalidad:").SemiBold();
                            table.Cell().Text(venta.Pasajero.Nacionalidad);

                            table.Cell().ColumnSpan(2).PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten4);

                            table.Cell().Text("Ruta:").SemiBold();
                            table.Cell().Text($"{venta.Asiento.Salida.Origen} a {venta.Asiento.Salida.Destino}").SemiBold();

                            table.Cell().Text("Fecha y Hora:").SemiBold();
                            table.Cell().Text($"{venta.Asiento.Salida.Fecha.ToShortDateString()} - {venta.Asiento.Salida.Hora.ToString(@"hh\:mm")}");

                            table.Cell().ColumnSpan(2).PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten4);

                            table.Cell().Text("Tarifa:").SemiBold();
                            table.Cell().Text($"${venta.Monto}");
                        });

                        col.Spacing(15);

                        col.Item().AlignCenter().Background("#548383").Padding(10).Text($"ASIENTO N° {venta.Asiento.Numero}")
                           .FontSize(18).SemiBold().FontColor(Colors.White);
                    });

                    page.Footer().AlignCenter().Column(col =>
                    {
                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        col.Item().PaddingTop(5).Text($"Token: {venta.Token_Boletero}").FontSize(8).FontColor(Colors.Grey.Medium);
                        col.Item().Text("Este boleto es personal e intransferible. Presentar CI al abordar.").FontSize(8).FontColor(Colors.Grey.Medium);
                    });
                });
            });

            byte[] pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Boleto_{venta.Documento_Pasajero}_Asiento{venta.ID_Asiento}.pdf");
        }
    }
}