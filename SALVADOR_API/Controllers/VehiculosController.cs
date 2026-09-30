using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Data;
using SALVADOR_API.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace SALVADOR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VehiculosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetVehiculos()
        {
            var vehiculos = await _context.Vehiculos
                .Select(v => new VehiculoDto { Placa = v.Placa, Capacidad = v.Capacidad })
                .ToListAsync();

            return Ok(vehiculos);
        }
    }
}
