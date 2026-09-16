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
        // =========================================================================
        // GET: Mostrar la pantalla del formulario a la cajera
        // =========================================================================
        [HttpGet]
        public async Task<IActionResult> FormularioVenta(int idAsiento)
        {
            var asiento = await _context.Asientos
                .Include(a => a.Venta)
                .FirstOrDefaultAsync(a => a.ID_Asiento == idAsiento);

            // Si el asiento no existe o ya fue vendido, lo rebotamos al croquis
            if (asiento == null || asiento.Venta != null)
            {
                return RedirectToAction("SeleccionarAsiento", new { idSalida = 1 });
            }

            // Preparamos los datos básicos para que Mariana los use en su vista
            var viewModel = new VentaPresencialViewModel
            {
                ID_Asiento = asiento.ID_Asiento,
                NumeroAsiento = asiento.Numero
            };

            return View(viewModel); // Esto llamará al HTML que hará Mariana
        }

        // =========================================================================
        // POST: Tu tarea principal -> Procesar la Venta y Registrar Migración
        // =========================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarVentaPresencial(VentaPresencialViewModel model)
        {
            // 1. Filtro de Seguridad: ¿Faltan datos que Mariana debía validar?
            if (!ModelState.IsValid)
            {
                return View("FormularioVenta", model); // Le devolvemos el formulario con errores
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 2. Buscamos el asiento y validamos que siga libre
                var asiento = await _context.Asientos
                    .Include(a => a.Salida)
                    .FirstOrDefaultAsync(a => a.ID_Asiento == model.ID_Asiento);

                if (asiento == null || asiento.Estado == "Vendido")
                {
                    ModelState.AddModelError("", "El asiento ya fue vendido o no está disponible.");
                    return View("FormularioVenta", model);
                }

                // 3. Lógica del Pasajero (Registrar o Actualizar)
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

                // 4. Lógica de la Venta
                var nuevaVenta = new Venta
                {
                    ID_Asiento = model.ID_Asiento,
                    Documento_Pasajero = model.Documento,
                    Fecha_Transaccion = DateTime.Now,
                    Monto = asiento.Salida.Tarifa,
                    Metodo_Pago = "Efectivo",
                    Token_Boletero = Guid.NewGuid().ToString() // Token único de impresión
                };
                _context.Ventas.Add(nuevaVenta);

                // 5. TU TAREA CLAVE: Cambiar el estado en la base de datos
                asiento.Estado = "Vendido";
                _context.Asientos.Update(asiento);

                // Guardamos la venta y el cambio de estado, y confirmamos
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // 6. Éxito: Le pasamos la venta a Erick para que él imprima el PDF
                // (Este método "GenerarBoletoPdf" lo creará Erick en su tarea)
                return RedirectToAction("GenerarBoletoPdf", new { idVenta = nuevaVenta.ID_Venta });
            }
            catch (DbUpdateException)
            {
                // El escudo de Diego bloqueó la base de datos por concurrencia
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Error: El asiento fue vendido a otra persona en este instante.");
                return View("FormularioVenta", model);
            }
        }
        // No olvides agregar este using arriba: using Rotativa.AspNetCore;

        // =========================================================================
        // GET: Generar e imprimir el Boleto en PDF (Tarea de Erick)
        // =========================================================================
        [HttpGet]

        // =========================================================================
        // GET: Generar e imprimir el Boleto en PDF con QuestPDF (Tarea de Erick)
        // =========================================================================
        [HttpGet]
        public async Task<IActionResult> GenerarBoletoPdf(int idVenta)
        {
            // 1. La Consulta Maestra (Se mantiene igual)
            var venta = await _context.Ventas
                .Include(v => v.Pasajero)
                .Include(v => v.Asiento)
                    .ThenInclude(a => a.Salida)
                .FirstOrDefaultAsync(v => v.ID_Venta == idVenta);

            if (venta == null) return NotFound("No se encontró el boleto solicitado.");

            // 2. Generación del PDF con QuestPDF usando C# puro
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5); // Tamaño ticket/medio oficio
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));
                    // HEADER
                    page.Header().Column(col =>
                    {
                        col.Item().AlignCenter().Text("SALVADOR SUPER CLAS")
                           .SemiBold().FontSize(20).FontColor("#548383"); // Color Teal
                        col.Item().AlignCenter().Text("Boleto de Viaje Oficial").Underline();
                        col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    });

                    // BODY (Contenido del boleto)
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Spacing(5);

                        // Usamos una tabla para organizar los datos
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(100); // Columna de etiquetas
                                columns.RelativeColumn();    // Columna de valores
                            });

                            // Filas de datos
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

                        // Bloque destacado del Asiento
                        col.Item().AlignCenter().Background("#548383").Padding(10).Text($"ASIENTO N° {venta.Asiento.Numero}")
                           .FontSize(18).SemiBold().FontColor(Colors.White);
                    });

                    // FOOTER
                    page.Footer().AlignCenter().Column(col =>
                    {
                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        col.Item().PaddingTop(5).Text($"Token: {venta.Token_Boletero}").FontSize(8).FontColor(Colors.Grey.Medium);
                        col.Item().Text("Este boleto es personal e intransferible. Presentar CI al abordar.").FontSize(8).FontColor(Colors.Grey.Medium);
                    });
                });
            });

            // 3. Renderizamos el PDF en memoria y lo devolvemos como archivo
            byte[] pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Boleto_{venta.Documento_Pasajero}_Asiento{venta.ID_Asiento}.pdf");
        }
    }
}