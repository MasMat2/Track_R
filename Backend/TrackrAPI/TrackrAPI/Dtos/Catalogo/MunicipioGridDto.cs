using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Catalogo
{
    public class MunicipioGridDto
    {
        public int IdMunicipio { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreEstado { get; set; } = null!;
        public string NombrePais { get; set; } = null!;
        public string Clave { get; set; } = null!;
    }
}
