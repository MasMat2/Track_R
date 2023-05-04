namespace TrackrAPI.Dtos.Inventario
{
    public class DomicilioGridDto
    {
        public int IdDomicilio { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public int IdEstado { get; set; }
        public string Direccion { get; set; }
        public int? IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
    }
}
