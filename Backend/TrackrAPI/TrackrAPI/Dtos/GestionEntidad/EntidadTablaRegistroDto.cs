using System.Collections.Generic;

namespace TrackrAPI.Dtos.GestionEntidad
{
    public class EntidadTablaRegistroDto
    {
        public int Numero { get; set; }
        public int IdEntidadEstructura { get; set; } // Sección
        public int IdTabla { get; set; }
        public List<TablaValorDto> Valores { get; set; }
    }

    public class TablaValorDto
    {
        public int IdEntidadEstructuraTablaValor { get; set; }
        public string ClaveCampo { get; set; }
        public string Valor { get; set; }
    }
}