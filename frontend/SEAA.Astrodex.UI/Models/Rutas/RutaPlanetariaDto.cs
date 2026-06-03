namespace SEAA.Astrodex.UI.Models.Rutas
{
    public class RutaPlanetariaDto
    {
        public string planetaOrigen { get; set; } = "";

        public string planetaDestino { get; set; } = "";

        public RutaDirectaDto? rutaDirecta { get; set; }

        public RutaPorTramosDto? rutaPorTramos { get; set; }
    }
}
