using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SALVADOR_SUPER_CLAS.ApiModels;
using SALVADOR_SUPER_CLAS.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SALVADOR_SUPER_CLAS.Controllers
{
    [AllowAnonymous]
    public class CuentaController : Controller
    {
        private readonly HttpClient _httpClient;

        public CuentaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SalvadorApi");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UsuarioLoginResultDto? resultado;
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Usuarios/login", new
                {
                    NombreUsuario = model.NombreUsuario,
                    Contrasena = model.Contrasena
                });
                resultado = await response.Content.ReadFromJsonAsync<UsuarioLoginResultDto>();
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "No se pudo conectar con el servidor (SALVADOR_API). Verifica que la API esté ejecutándose.");
                return View(model);
            }

            if (resultado == null || !resultado.Success || resultado.ID_Usuario == null)
            {
                ModelState.AddModelError("", resultado?.Message ?? "No se pudo iniciar sesión.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.NombreUsuario),
                new Claim("ID_Usuario", resultado.ID_Usuario.Value.ToString()),
                new Claim("NombreCompleto", resultado.NombreCompleto ?? model.NombreUsuario),
                new Claim(ClaimTypes.Role, resultado.Rol ?? "")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
