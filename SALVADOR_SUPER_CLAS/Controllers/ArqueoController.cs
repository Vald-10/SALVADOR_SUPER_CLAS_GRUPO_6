using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ArqueoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ArqueoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SalvadorApi");
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime? fecha)
        {
            string url = fecha.HasValue ? $"api/Arqueo/cajas?fecha={fecha.Value:yyyy-MM-dd}" : "api/Arqueo/cajas";
            var cajas = await _httpClient.GetFromJsonAsync<List<CierreCajaDto>>(url);

            var viewModel = new ArqueoViewModel
            {
                Fecha = fecha,
                Cajas = cajas ?? new List<CierreCajaDto>()
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Resumen(int id)
        {
            var response = await _httpClient.GetAsync($"api/Cajas/{id}/arqueo");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "La caja solicitada no existe.";
                return RedirectToAction("Index");
            }

            var arqueo = await response.Content.ReadFromJsonAsync<CierreCajaDto>();
            return View(arqueo);
        }
    }
}
