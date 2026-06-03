namespace SEAA.Astrodex.UI.Models.Historial
{
    public class HistorialResponseDto
    {
        public int pagina { get; set; }

        public int tamanio { get; set; }

        public int cantidad { get; set; }

        public List<HistorialDto> historial { get; set; }
            = [];
    }
}
