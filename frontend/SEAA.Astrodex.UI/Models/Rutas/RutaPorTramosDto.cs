namespace SEAA.Astrodex.UI.Models.Rutas
{
    public class RutaPorTramosDto
    {
        public string descripcion { get; set; } = "";

        public int cantidadTramos { get; set; }

        public double distanciaTotal { get; set; }

        public string unidadMedida { get; set; } = "";

        public List<TramoRutaDto> tramos { get; set; }
            = new();
    }
}
