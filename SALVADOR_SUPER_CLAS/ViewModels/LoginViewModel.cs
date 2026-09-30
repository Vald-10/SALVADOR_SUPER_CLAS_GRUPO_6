using System.ComponentModel.DataAnnotations;

namespace SALVADOR_SUPER_CLAS.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string NombreUsuario { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = null!;
    }
}
