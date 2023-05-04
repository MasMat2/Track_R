using System.Collections.Generic;

namespace TrackrAPI.Dtos.Catalogo
{
    public class MercadoFormularioDto
    {
        public int IdMercado { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int IdGiroComercial { get; set; }
        public List<CompaniaDto> Companias { get; set;}
    }
}