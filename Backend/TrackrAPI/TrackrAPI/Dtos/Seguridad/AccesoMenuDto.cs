using System.Collections.Generic;

namespace TrackrAPI.Dtos.Seguridad
{
    public class AccesoMenuDto
    {
        public int IdAcceso { get; set; }
        public int? IdAccesoPadre { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Url { get; set; }
        public string ClaseIcono { get; set; }
        public string UrlImagen { get; set; }
        public List<AccesoMenuDto> Hijos { get; set; }
        public string ClaveRolAcceso { get; set; }
        public string ClaveTipoAcceso { get; set; }
    }
}
