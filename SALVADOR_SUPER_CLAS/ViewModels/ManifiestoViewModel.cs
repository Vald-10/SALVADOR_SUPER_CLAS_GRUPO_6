using System;
using System.Collections.Generic;

namespace SALVADOR_SUPER_CLAS.ViewModels
{
    public class ManifiestoViewModel
    {
        public int ID_Salida { get; set; }
        public string Placa_Vehiculo { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }

        public List<PasajeroManifiesto> Pasajeros { get; set; } = new List<PasajeroManifiesto>();
    }

    public class PasajeroManifiesto
    {
        public int NumeroAsiento { get; set; }
        public string Documento { get; set; } = null!;
        public string Nombre_Completo { get; set; } = null!;
        public string Nacionalidad { get; set; } = null!;
        public string Genero { get; set; } = null!;
    }
}