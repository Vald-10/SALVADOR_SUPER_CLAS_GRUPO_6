using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles = Roles.Administrador)]
    public class SalidasController : Controller
    {
        private readonly HttpClient _httpClient;

        public SalidasController(IHttpClientFactory httpClientFactory)
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
        public async Task<IActionResult> Crear()
        {
            var viewModel = new SalidaFormViewModel
            {
                Fecha = DateTime.Today.AddDays(1),
                BusesDisponibles = await ObtenerBusesAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(SalidaFormViewModel model)
        {
            ValidarFormulario(model, esNueva: true);

            if (!ModelState.IsValid)
            {
                model.BusesDisponibles = await ObtenerBusesAsync();
                return View(model);
            }

            var response = await _httpClient.PostAsJsonAsync("api/Salidas", ConvertirADto(model));

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", await ApiHelper.LeerMensajeAsync(response, "No se pudo crear la salida. Revisa los datos e intenta de nuevo."));
                model.BusesDisponibles = await ObtenerBusesAsync();
                return View(model);
            }

            TempData["Exito"] = $"Salida {model.Origen} - {model.Destino} del {model.Fecha:dd/MM/yyyy} programada correctamente.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var response = await _httpClient.GetAsync($"api/Salidas/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "El viaje que buscas no existe.";
                return RedirectToAction("Index");
            }

            var salida = await response.Content.ReadFromJsonAsync<SalidaDetalleDto>();

            var viewModel = new SalidaFormViewModel
            {
                ID_Salida = salida!.ID_Salida,
                Placa_Vehiculo = salida.Placa_Vehiculo,
                Origen = salida.Origen,
                Destino = salida.Destino,
                Fecha = salida.Fecha,
                Hora = salida.Hora,
                Tarifa = salida.Tarifa,
                Asientos_Vendidos = salida.Asientos.Count(a => a.Vendido),
                BusesDisponibles = await ObtenerBusesAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, SalidaFormViewModel model)
        {
            model.ID_Salida = id;
            ValidarFormulario(model, esNueva: false);

            if (!ModelState.IsValid)
            {
                model.BusesDisponibles = await ObtenerBusesAsync();
                return View(model);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/Salidas/{id}", ConvertirADto(model));

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", await ApiHelper.LeerMensajeAsync(response, "No se pudo actualizar la salida. Revisa los datos e intenta de nuevo."));
                model.BusesDisponibles = await ObtenerBusesAsync();
                return View(model);
            }

            TempData["Exito"] = "Salida actualizada correctamente.";
            return RedirectToAction("Index");
        }

        private void ValidarFormulario(SalidaFormViewModel model, bool esNueva)
        {
            if (!string.IsNullOrWhiteSpace(model.Origen) && !string.IsNullOrWhiteSpace(model.Destino)
                && string.Equals(model.Origen.Trim(), model.Destino.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(nameof(model.Destino), "El destino no puede ser igual al origen.");
            }

            if (esNueva && model.Fecha.Date < DateTime.Today)
            {
                ModelState.AddModelError(nameof(model.Fecha), "No se puede programar una salida en una fecha pasada.");
            }
        }

        private static SalidaDto ConvertirADto(SalidaFormViewModel model)
        {
            return new SalidaDto
            {
                Placa_Vehiculo = model.Placa_Vehiculo,
                Origen = model.Origen.Trim(),
                Destino = model.Destino.Trim(),
                Fecha = model.Fecha.Date,
                Hora = model.Hora,
                Tarifa = model.Tarifa ?? 0m
            };
        }

        private async Task<List<VehiculoOptionViewModel>> ObtenerBusesAsync()
        {
            var vehiculos = await _httpClient.GetFromJsonAsync<List<VehiculoDto>>("api/Vehiculos");
            return (vehiculos ?? new List<VehiculoDto>())
                .Select(v => new VehiculoOptionViewModel { Placa = v.Placa, Capacidad = v.Capacidad })
                .ToList();
        }
    }
}
