using SEAA.Astrodex.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEAA.Astrodex.Core.Interfaces
{
    public interface IRutaService
    {
        Task<RutaPlanetariaDto?> CalcularRutaAsync(
            string idOrigen, string idDestino);
    }
}
