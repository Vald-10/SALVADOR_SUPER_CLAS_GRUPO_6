using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SALVADOR_SUPER_CLAS.Utils
{
    public static class ApiHelper
    {
        public static async Task<string> LeerMensajeAsync(HttpResponseMessage response, string mensajePorDefecto)
        {
            string texto = await response.Content.ReadAsStringAsync();
            texto = texto.Trim().Trim('"');

            if (string.IsNullOrWhiteSpace(texto) || texto.StartsWith("{"))
            {
                return mensajePorDefecto;
            }
            return texto;
        }

        public static int ObtenerIdUsuario(ClaimsPrincipal usuario)
        {
            var claim = usuario.FindFirst("ID_Usuario");
            return claim != null && int.TryParse(claim.Value, out int id) ? id : 0;
        }

        public static string Bs(decimal monto)
        {
            return "Bs. " + monto.ToString("N2", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
