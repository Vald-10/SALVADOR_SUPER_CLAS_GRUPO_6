using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SALVADOR_SUPER_CLAS.ApiModels;
using SALVADOR_SUPER_CLAS.Utils;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SALVADOR_SUPER_CLAS.Controllers
{
    [Authorize(Roles = Roles.GerenciaYAdministracion)]
    public class ReportesController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReportesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SalvadorApi");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reporte = await _httpClient.GetFromJsonAsync<ReporteDto>("api/Reportes/resumen");
            return View(reporte ?? new ReporteDto());
        }
    }
}
