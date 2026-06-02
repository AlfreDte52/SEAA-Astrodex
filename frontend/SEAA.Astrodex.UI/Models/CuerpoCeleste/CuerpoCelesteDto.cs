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

        public double densidad { get; set; }

        public double velocidadEscape { get; set; }

        public double orbita { get; set; }

        public double rotacion { get; set; }

        public double inclinacionAxial { get; set; }

        public double masaValor { get; set; }

        public int masaExponente { get; set; }

        public double volumenValor { get; set; }

        public int volumenExponente { get; set; }

        public string descubiertoPor { get; set; }
            = string.Empty;

        public string fechaDescubrimiento { get; set; }
            = string.Empty;
    }
}
