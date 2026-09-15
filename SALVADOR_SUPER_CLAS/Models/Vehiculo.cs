using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SALVADOR_SUPER_CLAS.Models
{
    public class Vehiculo
    {
        [Key]
        [MaxLength(15)]
        public string Placa { get; set; } = null!;

        public int Capacidad { get; set; }

        public ICollection<Salida> Salidas { get; set; } = new List<Salida>();
    }
}