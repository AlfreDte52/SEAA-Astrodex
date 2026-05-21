using QuikGraph;
using QuikGraph.Algorithms.ShortestPath;
using SEAA.Astrodex.Core.Entities;
using SEAA.Astrodex.Infrastructure.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEAA.Astrodex.Infrastructure.Graphs
{
    public class GrafoPlanetario
    {
        private readonly EstrategiaDistancia _estrategiaDistancia;

        public GrafoPlanetario(EstrategiaDistancia estrategiaDistancia)
        {
            _estrategiaDistancia = estrategiaDistancia;
        }

        // Construye un grafo no dirigido conectando todos los planetas entre sí
        // El peso de cada arista es la distancia promedio calculada con la estrategia
        public (UndirectedGraph<string, TaggedEdge<string, double>> grafo,
                Dictionary<TaggedEdge<string, double>, double> pesos)
            Construir(List<CuerpoCeleste> planetas)
        {
            var grafo = new UndirectedGraph<string, TaggedEdge<string, double>>();
            var pesos = new Dictionary<TaggedEdge<string, double>, double>();

            // Agrega cada planeta como nodo
            foreach (var p in planetas)
                grafo.AddVertex(p.Id);

            // Conecta cada par de planetas con su distancia calculada
            for (int i = 0; i < planetas.Count; i++)
            {
                for (int j = i + 1; j < planetas.Count; j++)
                {
                    var a = planetas[i];
                    var b = planetas[j];

                    var relacion = _estrategiaDistancia.Ejecutar(a, b);

                    if (relacion.ValorCalculado <= 0)
                        continue;

                    var arista = new TaggedEdge<string, double>(
                        a.Id, b.Id, relacion.ValorCalculado);

                    grafo.AddEdge(arista);
                    pesos[arista] = relacion.ValorCalculado;
                }
            }

            return (grafo, pesos);
        }

        // Encuentra la ruta más corta entre dos planetas usando Dijkstra
        // Si el grafo es completo (todos conectados), retorna el camino directo
        // Por eso usamos la ruta secuencial por orden alfabético/posición
        // como alternativa "por tramos"
        public List<string> BuscarRutaMasCorta(
            UndirectedGraph<string, TaggedEdge<string, double>> grafo,
            Dictionary<TaggedEdge<string, double>, double> pesos,
            string origen, string destino)
        {
            Func<TaggedEdge<string, double>, double> pesoFunc =
                edge => pesos[edge];

            var dijkstra = new UndirectedDijkstraShortestPathAlgorithm
                <string, TaggedEdge<string, double>>(grafo, pesoFunc);

            var predecesores = new Dictionary<string, TaggedEdge<string, double>>();

            dijkstra.TreeEdge += (sender, args) =>
            {
                predecesores[args.Target] = args.Edge;
            };

            dijkstra.Compute(origen);

            // Reconstruye el camino desde destino hasta origen
            var camino = new List<string>();
            var actual = destino;

            while (actual != origen)
            {
                camino.Insert(0, actual);

                if (!predecesores.ContainsKey(actual))
                    return new List<string>();

                var arista = predecesores[actual];
                actual = arista.Source == actual ? arista.Target : arista.Source;
            }

            camino.Insert(0, origen);
            return camino;
        }

        // Construye la ruta secuencial pasando por todos los planetas
        // intermedios en orden por semimajorAxis
        public List<string> RutaSecuencial(
            List<CuerpoCeleste> planetas, string origen, string destino)
        {
            var ordenados = planetas
                .OrderBy(p => p.SemimajorAxis)
                .Select(p => p.Id)
                .ToList();

            var indiceOrigen = ordenados.IndexOf(origen);
            var indiceDestino = ordenados.IndexOf(destino);

            if (indiceOrigen == -1 || indiceDestino == -1)
                return new List<string>();

            if (indiceOrigen < indiceDestino)
                return ordenados.GetRange(indiceOrigen, indiceDestino - indiceOrigen + 1);
            else
                return ordenados.GetRange(indiceDestino, indiceOrigen - indiceDestino + 1)
                    .AsEnumerable().Reverse().ToList();
        }
    }
}
