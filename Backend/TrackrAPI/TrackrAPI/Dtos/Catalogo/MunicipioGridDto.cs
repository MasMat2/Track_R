using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Catalogo
{
    public class MunicipioGridDto
    {
        public int IdMunicipio { get; set; }
        public string Nombre { get; set; }
        public string NombreEstado { get; set; }
        public string NombrePais { get; set; }
        public string Clave { get; set; }

        public MunicipioGridDto(int IdMunicipio, string nombre, string NombreEstado, string NombrePais, string Clave)
        {
            this.IdMunicipio = IdMunicipio;
            this.Nombre = nombre;
            this.NombreEstado = NombreEstado;
            this.NombrePais = NombrePais;
            this.Clave = Clave;
        }
    }
}
