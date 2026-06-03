namespace SEAA.Astrodex.UI.Models.Historial
{
    public class HistorialDto
    {
        public int id { get; set; }

        public string tipoConsulta { get; set; }
            = string.Empty;

        public DateTime fechaConsulta { get; set; }

        public string cuerpoCelesteId { get; set; }
            = string.Empty;
    }
}
