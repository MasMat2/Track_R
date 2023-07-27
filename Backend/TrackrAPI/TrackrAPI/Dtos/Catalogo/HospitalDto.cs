using System;

namespace TrackrAPI.Dtos.Catalogo
{
    public class HospitalDto
    {
        public int IdHospital { get; set; }
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaContableActual { get; set; }
        public int? IdUsuarioGerente { get; set; }
        public int? IdCompania { get; set; }
        public int? IdListaPrecioGeneral { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdEstado { get; set; }
        public int? IdPais { get; set; }
        public string Ciudad { get; set; }
        public int? IdBanco { get; set; }
        public string Cuenta { get; set; }
        public string Clabe { get; set; }
        public string PortalWeb { get; set; }
        public string Rfc { get; set; }
        public int? IdRegimenFiscal { get; set; }
        public int? IdLada { get; set; }
        public string NombreComercial { get; set; }
        public string EntreCalles { get; set; }
        public string NombreBanco { get; set; }
        public bool Predeterminada { get; set; }
        public int? IdListaPrecioDefault { get; set; }
        public int? IdListaPrecioLinea { get; set; }
        public int? IdAlmacenProduccion { get; set; }
        public int? IdAlmacenCaduco { get; set; }
    }
}
