using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SALVADOR_SUPER_CLAS.ApiModels;
using SALVADOR_SUPER_CLAS.Utils;
using SALVADOR_SUPER_CLAS.ViewModels;

namespace SALVADOR_SUPER_CLAS.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SalvadorApi");
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Roles.GerenteOperaciones))
            {
                return RedirectToAction("Index", "Reportes");
            }

            if (User.IsInRole(Roles.Boletero))
            {
                int idUsuario = ApiHelper.ObtenerIdUsuario(User);
                if (idUsuario == 0)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Login", "Cuenta");
                }

                var response = await _httpClient.GetAsync($"api/Cajas/actual/{idUsuario}");
                CierreCajaDto? arqueo = null;
                if (response.IsSuccessStatusCode)
                {
                    var caja = await response.Content.ReadFromJsonAsync<CajaDto>();
                    arqueo = await _httpClient.GetFromJsonAsync<CierreCajaDto>($"api/Cajas/{caja!.ID_Caja}/arqueo");
                }
                return View("InicioBoletero", arqueo);
            }

            var salidas = await _httpClient.GetFromJsonAsync<List<SalidaResumenDto>>("api/Salidas") ?? new List<SalidaResumenDto>();
            return View(salidas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
