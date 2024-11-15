namespace TrackrAPI.Dtos.Catalogo
{
    public class MunicipioDto
    {
        public int IdMunicipio { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int IdEstado { get; set; }
        public string Clave { get; set; } = string.Empty;   
        public string ClaveEstado { get; set; } = string.Empty;
    }
}