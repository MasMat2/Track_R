using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Catalogo
{
    public class CodigoPostalGridDto
    {
        public int IdCodigoPostal { get; set; }
        public string CodigoPostal1 { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public int IdMunicipio { get; set; }
        public string Estado { get; set; }

        public CodigoPostalGridDto(int idCodigoPostal, string codigoPostal1, string colonia,string municipio, int idMunicipio , string estado)
        {
            this.IdCodigoPostal = idCodigoPostal;
            this.CodigoPostal1 = codigoPostal1;
            this.Colonia = colonia;
            this.Municipio = municipio;
            this.IdMunicipio = idMunicipio;
            this.Estado = estado;

        }
    }
}
