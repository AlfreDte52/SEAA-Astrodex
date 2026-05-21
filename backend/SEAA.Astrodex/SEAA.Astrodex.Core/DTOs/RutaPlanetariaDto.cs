using System;
using System.Collections.Generic;
using System.Text;

namespace SEAA.Astrodex.Core.DTOs
{
    public class RutaPlanetariaDto
    {
        public string PlanetaOrigen { get; set; } = string.Empty;
        public string PlanetaDestino { get; set; } = string.Empty;

        public RutaDirectaDto RutaDirecta { get; set; } = new();
        public RutaPorTramosDto RutaPorTramos { get; set; } = new();
    }
}
