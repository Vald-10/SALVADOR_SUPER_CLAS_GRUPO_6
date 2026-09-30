using System.ComponentModel.DataAnnotations;

namespace SALVADOR_API.Models
{
    public class Usuario
    {
        [Key]
        public int ID_Usuario { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreUsuario { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string NombreCompleto { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ContrasenaHash { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Rol { get; set; } = null!;
    }
}
