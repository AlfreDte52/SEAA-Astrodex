using System;
using System.Collections.Generic;
using System.Text;

namespace SEAA.Astrodex.Core.DTOs
{
    public class HistorialResponseDto
    {
        public int Id { get; set; }
        public string TipoConsulta { get; set; } = string.Empty;
        public DateTime FechaConsulta { get; set; }
        public string CuerpoCelesteId { get; set; } = string.Empty;
    }
}
