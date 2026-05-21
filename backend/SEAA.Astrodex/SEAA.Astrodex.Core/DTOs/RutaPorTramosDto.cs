using System;
using System.Collections.Generic;
using System.Text;

namespace SEAA.Astrodex.Core.DTOs
{
    public class RutaPorTramosDto
    {
        public string Descripcion { get; set; } = string.Empty;
        public int CantidadTramos { get; set; }
        public double DistanciaTotal { get; set; }
        public string UnidadMedida { get; set; } = "km";
        public List<TramoRutaDto> Tramos { get; set; } = new();
    }
}
