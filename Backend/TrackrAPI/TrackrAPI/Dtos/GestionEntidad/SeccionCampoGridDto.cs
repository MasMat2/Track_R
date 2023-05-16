
namespace TrackrAPI.Dtos.GestionEntidad
{
    public class SeccionCampoGridDto
    {
        public int IdSeccionCampo { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public int IdDominio { get; set; }
        public int IdSeccion { get; set; }
        public bool Requerido { get; set; }
        public int? Orden { get; set; }
        public int TamanoColumna { get; set; }

        public string NombreSeccion { get; set; }

        public string NombreDominio { get; set; }

        public SeccionCampoGridDto(int idSeccionCampo,string clave , int idSeccion, string descripcion,
            int tamanoColumna, bool requerido, string nombreSeccion, string nombreDominio, int idDominio, int? orden)
        {
            IdSeccionCampo = idSeccionCampo;
            Clave = clave;
            IdSeccion = idSeccion;
            Descripcion = descripcion;
            TamanoColumna = tamanoColumna;
            Requerido = requerido;
            NombreSeccion = nombreSeccion;
            NombreDominio = nombreDominio;
            IdDominio = idDominio;
            Orden = orden;
        }
    }
}
