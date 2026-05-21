using System;
using System.Collections.Generic;
using System.Text;

// Infrastructure/Services/RutaService.cs
using SEAA.Astrodex.Core.Constants;
using SEAA.Astrodex.Core.DTOs;
using SEAA.Astrodex.Core.Entities;
using SEAA.Astrodex.Core.Interfaces;
using SEAA.Astrodex.Infrastructure.Graphs;
using SEAA.Astrodex.Infrastructure.Strategies;

namespace SEAA.Astrodex.Infrastructure.Services
{
    public class RutaService : IRutaService
    {
        private readonly ICuerpoCelesteRepository _cuerpoRepository;
        private readonly IRelacionRepository _relacionRepository;
        private readonly GrafoPlanetario _grafo;
        private readonly EstrategiaDistancia _estrategiaDistancia;

        public RutaService(
            ICuerpoCelesteRepository cuerpoRepository,
            IRelacionRepository relacionRepository,
            GrafoPlanetario grafo,
            EstrategiaDistancia estrategiaDistancia)
        {
            _cuerpoRepository = cuerpoRepository;
            _relacionRepository = relacionRepository;
            _grafo = grafo;
            _estrategiaDistancia = estrategiaDistancia;
        }

        // Calcula dos rutas: la directa y la secuencial por tramos
        public async Task<RutaPlanetariaDto?> CalcularRutaAsync(
            string idOrigen, string idDestino)
        {
            // Obtiene origen y destino con Operación 1
            var origen = await _cuerpoRepository.ObtenerCuerpoCelesteAsync(idOrigen);
            var destino = await _cuerpoRepository.ObtenerCuerpoCelesteAsync(idDestino);

            if (origen == null || destino == null)
                return null;

            // Solo se permite si ambos son planetas que orbitan al Sol
            if (!origen.EsPlaneta || !destino.EsPlaneta)
                return null;

            if (!string.IsNullOrEmpty(origen.PlanetaPadreId) ||
                !string.IsNullOrEmpty(destino.PlanetaPadreId))
                return null;

            // Trae todos los planetas principales
            var planetas = await _cuerpoRepository.ObtenerPlanetasPrincipalesAsync();

            if (planetas.Count < 2)
                return null;

            var planetasDict = planetas.ToDictionary(p => p.Id, p => p);

            // Calcula ruta directa con la EstrategiaDistancia
            var relacionDirecta = _estrategiaDistancia.Ejecutar(origen, destino);
            var rutaDirecta = new RutaDirectaDto
            {
                Descripcion = $"{origen.NombreIngles} → {destino.NombreIngles}",
                Distancia = relacionDirecta.ValorCalculado,
                UnidadMedida = "km"
            };

            // Calcula ruta secuencial pasando por planetas intermedios
            var secuencia = _grafo.RutaSecuencial(planetas, origen.Id, destino.Id);

            var tramos = new List<TramoRutaDto>();
            double distanciaTotal = 0;

            for (int i = 0; i < secuencia.Count - 1; i++)
            {
                var planetaA = planetasDict[secuencia[i]];
                var planetaB = planetasDict[secuencia[i + 1]];

                var relacionTramo = _estrategiaDistancia.Ejecutar(planetaA, planetaB);
                distanciaTotal += relacionTramo.ValorCalculado;

                tramos.Add(new TramoRutaDto
                {
                    NumeroTramo = i + 1,
                    Origen = planetaA.Id,
                    Destino = planetaB.Id,
                    Distancia = relacionTramo.ValorCalculado,
                    UnidadMedida = "km"
                });
            }

            var rutaPorTramos = new RutaPorTramosDto
            {
                Descripcion = string.Join(" → ",
                    secuencia.Select(id => planetasDict[id].NombreIngles)),
                CantidadTramos = tramos.Count,
                DistanciaTotal = Math.Round(distanciaTotal, 2),
                UnidadMedida = "km",
                Tramos = tramos
            };

            // Guarda en BD: una fila para la directa
            var relacionesParaGuardar = new List<RelacionCeleste>
            {
                new RelacionCeleste
                {
                    TipoRelacion = TiposRelacion.ORBITAL,
                    ValorCalculado = relacionDirecta.ValorCalculado,
                    UnidadMedida = "km",
                    Descripcion = $"Ruta directa: {rutaDirecta.Descripcion}",
                    CuerpoOrigenId = origen.Id,
                    CuerpoDestinoId = destino.Id,
                    FechaConsulta = DateTime.Now
                }
            };

            // Guarda en BD: una fila por cada tramo
            foreach (var tramo in tramos)
            {
                relacionesParaGuardar.Add(new RelacionCeleste
                {
                    TipoRelacion = TiposRelacion.ORBITAL,
                    ValorCalculado = tramo.Distancia,
                    UnidadMedida = "km",
                    Descripcion = $"Tramo {tramo.NumeroTramo} de ruta: " +
                                  $"{tramo.Origen} → {tramo.Destino}",
                    CuerpoOrigenId = tramo.Origen,
                    CuerpoDestinoId = tramo.Destino,
                    FechaConsulta = DateTime.Now
                });
            }

            await _relacionRepository.GuardarRelacionesAsync(relacionesParaGuardar);

            // Registra en historial
            await _cuerpoRepository.RegistrarHistorialEnBdAsync(
                origen.Id, TiposConsulta.RUTA_PLANETARIA);

            return new RutaPlanetariaDto
            {
                PlanetaOrigen = origen.Id,
                PlanetaDestino = destino.Id,
                RutaDirecta = rutaDirecta,
                RutaPorTramos = rutaPorTramos
            };
        }
    }
}
