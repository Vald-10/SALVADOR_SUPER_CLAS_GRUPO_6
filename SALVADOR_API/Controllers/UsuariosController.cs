using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALVADOR_API.Data;
using SALVADOR_API.Dtos;
using SALVADOR_API.Utils;
using System.Threading.Tasks;

namespace SALVADOR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioLoginResultDto>> Login(UsuarioLoginRequestDto request)
        {
            var hash = PasswordHasher.Hash(request.Contrasena);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == request.NombreUsuario);

            if (usuario == null || usuario.ContrasenaHash != hash)
            {
                return Ok(new UsuarioLoginResultDto
                {
                    Success = false,
                    Message = "Usuario o contraseña incorrectos."
                });
            }

            return Ok(new UsuarioLoginResultDto
            {
                Success = true,
                Message = "Inicio de sesión exitoso.",
                ID_Usuario = usuario.ID_Usuario,
                NombreCompleto = usuario.NombreCompleto,
                Rol = usuario.Rol
            });
        }
    }
}
