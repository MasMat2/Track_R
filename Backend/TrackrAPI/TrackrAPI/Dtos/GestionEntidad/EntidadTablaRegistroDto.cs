using System.Collections.Generic;

namespace TrackrAPI.Dtos.GestionEntidad
{
    public class EntidadTablaRegistroDto
    {
        public int Numero { get; set; }
        public int IdEntidadEstructura { get; set; } // Sección
        public int IdTabla { get; set; }
        public List<TablaValorDto> Valores { get; set; } = new List<TablaValorDto>();
    }

    public class TablaValorDto
    {
        public int IdEntidadEstructuraTablaValor { get; set; }
        public string ClaveCampo { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public bool FueraDeRango { get; set; }
        public DateTime FechaMuestra { get; set; }
    }
}