using System;
using System.Collections.Generic;
using System.Text;

namespace SEAA.Astrodex.Core.DTOs
{
    public class RelacionHistorialDto
    {
        public int Id { get; set; }
        public string TipoRelacion { get; set; } = string.Empty;
        public double ValorCalculado { get; set; }
        public string UnidadMedida { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaConsulta { get; set; }
        public string CuerpoOrigenId { get; set; } = string.Empty;
        public string CuerpoDestinoId { get; set; } = string.Empty;
    }
}
