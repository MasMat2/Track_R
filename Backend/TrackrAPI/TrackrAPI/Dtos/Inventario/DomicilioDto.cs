using TrackrAPI.Dtos.Catalogo;

namespace TrackrAPI.Dtos.Inventario
{
    public class DomicilioDto
    {
        public int IdDomicilio { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public int IdPais { get; set; }
        public int IdEstado { get; set; }
        public int IdMunicipio { get; set; }
        public int IdLocalidad { get; set; }
        public int IdColonia { get; set; }
        public string Direccion { get; set; }
        public int? IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public EstadoDto Estado { get; set; }
        public string NombreSucursal { get; set; }
        public int? IdMetodoPago { get; set; }
        public int? IdUsuarioRepartidor { get; set; }
        public string EntreCalles { get; set; }
        public string OtraReferencia { get; set; }
    }
}
