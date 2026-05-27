namespace SEAA.Astrodex.UI.Models.CuerpoCeleste
{
    public class CuerpoCelesteDto
    {
        public string id { get; set; } = string.Empty;

        public string nombre { get; set; } = string.Empty;

        public string nombreIngles { get; set; }
            = string.Empty;

        public string tipoCuerpo { get; set; }
            = string.Empty;

        public bool esPlaneta { get; set; }

        public double gravedad { get; set; }

        public double radioMedio { get; set; }

        public double tempPromedio { get; set; }
    }
}
