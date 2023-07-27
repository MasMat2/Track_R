using System.Collections.Generic;

namespace TrackrAPI.Dtos.Contabilidad
{
    public class JerarquiaEstructuraArbolDto
    {
        public int IdJerarquiaEstructura { get; set; }
        public int? IdJerarquiaEstructuraPadre { get; set; }
        public string Cuenta { get; set; }
        public List<JerarquiaEstructuraArbolDto> Hijos { get; set; }

        // Extras
        public int? IdAcceso { get; set; }
        public int? IdSeccion { get; set; }
        public string TipoAcceso { get; set; }
    }
}
