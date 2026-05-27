namespace SEAA.Astrodex.UI.Models.CuerpoCeleste
{
    public class ApiResponseDto
    {
        public string fuente { get; set; } = string.Empty;

        public string tipo { get; set; } = string.Empty;

        public int pagina { get; set; }

        public int tamanio { get; set; }

        public List<CuerpoCelesteDto> cuerpos { get; set; }
            = new();
    }
}
