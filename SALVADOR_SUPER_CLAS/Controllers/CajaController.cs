using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SALVADOR_SUPER_CLAS.ApiModels;
using SALVADOR_SUPER_CLAS.Utils;
using SALVADOR_SUPER_CLAS.ViewModels;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SALVADOR_SUPER_CLAS.Controllers
{
    [Authorize(Roles = Roles.Boletero)]
    public class CajaController : Controller
    {
        private readonly HttpClient _httpClient;

        public CajaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SalvadorApi");
        }

        private async Task<CajaDto?> ObtenerCajaDeHoyAsync(int idUsuario)
        {
            var response = await _httpClient.GetAsync($"api/Cajas/actual/{idUsuario}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<CajaDto>();
        }

        [HttpGet]
        public async Task<IActionResult> Apertura()
        {
            int idUsuario = ApiHelper.ObtenerIdUsuario(User);
            if (idUsuario == 0)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Cuenta");
            }

            var caja = await ObtenerCajaDeHoyAsync(idUsuario);
            if (caja != null)
            {
                TempData["Info"] = caja.EstaAbierta
                    ? "Tu caja de hoy ya está abierta."
                    : "Tu caja de hoy ya fue cerrada. Solo se permite una caja por día.";
                return caja.EstaAbierta ? RedirectToAction("Index", "Venta") : RedirectToAction("Cierre");
            }

            return View(new AperturaCajaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apertura(AperturaCajaViewModel model)
        {
            model.Fecha_Apertura = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _httpClient.PostAsJsonAsync("api/Cajas/apertura", new AperturaCajaDto
            {
                ID_Usuario = ApiHelper.ObtenerIdUsuario(User),
                Fecha_Apertura = model.Fecha_Apertura,
                Monto_Inicial = model.Monto_Inicial ?? 0m
            });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", await ApiHelper.LeerMensajeAsync(response, "No se pudo abrir la caja."));
                return View(model);
            }

            TempData["Exito"] = $"Caja abierta con un monto inicial de {ApiHelper.Bs(model.Monto_Inicial ?? 0m)}. Ya puedes vender pasajes.";
            return RedirectToAction("Index", "Venta");
        }

        [HttpGet]
        public async Task<IActionResult> Cierre()
        {
            int idUsuario = ApiHelper.ObtenerIdUsuario(User);
            var caja = await ObtenerCajaDeHoyAsync(idUsuario);
            if (caja == null)
            {
                TempData["Info"] = "Todavía no abriste tu caja hoy.";
                return RedirectToAction("Apertura");
            }

            var arqueo = await _httpClient.GetFromJsonAsync<CierreCajaDto>($"api/Cajas/{caja.ID_Caja}/arqueo");
            return View(arqueo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cerrar(int idCaja)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Cajas/{idCaja}/cierre", new CerrarCajaRequestDto
            {
                ID_Usuario = ApiHelper.ObtenerIdUsuario(User)
            });

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = await ApiHelper.LeerMensajeAsync(response, "No se pudo cerrar la caja.");
            }
            else
            {
                TempData["Exito"] = "Caja cerrada correctamente. Este es el arqueo final de tu turno.";
            }

            return RedirectToAction("Cierre");
        }
    }
}
