namespace TrackrAPI.Dtos.Seguridad
{
    public class RolDto
    {
        public int IdRol { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Filtrado { get; set; }
        public int? IdCompania { get; set; }
    }
}
