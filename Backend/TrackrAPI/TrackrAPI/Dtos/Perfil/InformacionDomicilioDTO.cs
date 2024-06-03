using TrackrAPI.Dtos.GestionExpediente;

namespace TrackrAPI.Dtos.Perfil
{
    public class InformacionDomicilioDTO
    {
        public int? IdPais { get; set; }
        public int? IdEstado { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdLocalidad { get; set; }
        public int? IdColonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Calle { get; set; }
        public string EntreCalles { get; set; }
        public string NumeroInterior { get; set; }
        public string NumeroExterior { get; set; }

    }
}
