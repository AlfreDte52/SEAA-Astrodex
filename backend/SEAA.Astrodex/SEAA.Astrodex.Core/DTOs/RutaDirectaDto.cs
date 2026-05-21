using System;
using System.Collections.Generic;
using System.Text;

namespace SEAA.Astrodex.Core.DTOs
{
    public class RutaDirectaDto
    {
        public string Descripcion { get; set; } = string.Empty;
        public double Distancia { get; set; }
        public string UnidadMedida { get; set; } = "km";
    }
}
