using System.Collections.Generic;

namespace TrackrAPI.Dtos.Seguridad
{
    public class JerarquiaAccesoDto
    {
        public int IdJerarquiaAcceso { get; set; }
        public int IdCompania { get; set; }
        public string Nombre { get; set; }
        public string NombreTipoCompania { get; set; }
        public List<int> IdsTipoCompania { get; set; }
    }
}
