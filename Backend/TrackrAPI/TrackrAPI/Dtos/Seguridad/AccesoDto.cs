namespace TrackrAPI.Dtos.Seguridad
{
    public class AccesoDto
    {
        public int IdAcceso { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public int? OrdenMenu { get; set; }
        public int IdTipoAcceso { get; set; }
        public int? IdAccesoPadre { get; set; }
        public int? IdIcono { get; set; }
        public bool TieneAcceso { get; set; }
        public int? IdRolAcceso { get; set; }
        public string UrlVideoAyuda { get; set; }
        public string Descripcion { get; set; }

    }
}
