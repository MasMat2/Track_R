using System.Collections.Generic;

namespace TrackrAPI.Dtos.Seguridad
{
    public class PerfilDto
    {
        public int IdPerfil { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string NombreTipoCompania { get; set; }
        public string NombreJerarquia { get; set; }
        public int IdCompania { get; set; }
        public int? IdTipoCompania { get; set; }
        public int? IdJerarquiaAcceso { get; set; }
        public int[] IdsAcceso { get; set; }
    }
}
