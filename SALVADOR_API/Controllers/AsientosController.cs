using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Data;
using SALVADOR_API.Dtos;
using System.Threading.Tasks;

namespace SALVADOR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsientosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AsientosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AsientoDto>> GetAsiento(int id)
        {
            var asiento = await _context.Asientos
                .Include(a => a.Venta)
                .FirstOrDefaultAsync(a => a.ID_Asiento == id);

            if (asiento == null)
            {
                return NotFound("El asiento solicitado no existe.");
            }

            var dto = new AsientoDto
            {
                ID_Asiento = asiento.ID_Asiento,
                ID_Salida = asiento.ID_Salida,
                Numero = asiento.Numero,
                Vendido = asiento.Venta != null
            };

            return Ok(dto);
        }
    }
}
