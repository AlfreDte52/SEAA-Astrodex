namespace SEAA.Astrodex.UI.Models.Rutas
{
    public class TramoRutaDto
    {
        public int numeroTramo { get; set; }

        public string origen { get; set; } = "";

        public string destino { get; set; } = "";

        public double distancia { get; set; }

        public string unidadMedida { get; set; } = "";
    }
}
