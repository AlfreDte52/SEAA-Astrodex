using System;
using System.Collections.Generic;
using System.Text;

namespace SEAA.Astrodex.Core.DTOs
{
    public class TramoRutaDto
    {
        public int NumeroTramo { get; set; }
        public string Origen { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public double Distancia { get; set; }
        public string UnidadMedida { get; set; } = "km";
    }
}
