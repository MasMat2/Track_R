using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Dtos.GestionEntidad
{
    public class EntidadEstructuraDto
    {
        public int IdEntidadEstructura { get; set; }
        public string? Nombre { get; set; }
        public string? Clave { get; set; }
        public bool? Tabulacion { get; set; }
        public int IdEntidad { get; set; }
        public int? IdSeccion { get; set; }
        public int? IdEntidadEstructuraPadre { get; set; }
        public bool EsTabla { get; set; }
        public int? IdTipoWidget {get ; set;}
        public int? IdIcono { get; set; }

        public List<EntidadEstructuraDto> Hijos { get; set; }
        public List<SeccionCampo> Campos { get; set; }
    }
}
