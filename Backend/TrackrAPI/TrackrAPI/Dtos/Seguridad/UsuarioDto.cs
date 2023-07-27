using System;
using System.Collections.Generic;

namespace TrackrAPI.Dtos.Seguridad
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreCompleto { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public int? IdPais { get; set; }
        public int? IdEstado { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdLocalidad { get; set; }
        public int? IdColonia { get; set; }
        public string Ciudad { get; set; }
        public string TelefonoMovil { get; set; }
        public string ContrasenaActualizada { get; set; }
        public int? IdPerfil { get; set; }
        public int? IdHospital { get; set; }
        public int? IdCompania { get; set; }
        public int IdTipoUsuario { get; set; }
        public string Username { get; set; }
        public string ImagenBase64 { get; set; }
        public string ImagenTipoMime { get; set; }
        public bool Habilitado { get; set; }
        public int? IdTituloAcademico { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdArea { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string NumeroInterior { get; set; }
        public string NumeroExterior { get; set; }
        public string CodigoPostal { get; set; }
        public string CorreoPersonal { get; set; }
        public string NombrePerfil { get; set; }
        public string Cedula { get; set; }
        public int? IdPuntoVenta { get; set; }
        public string Direccion { get; set; }
        public string Rfc { get; set; }
        public decimal? SueldoDiario { get; set; }
        public string ClaveTipoUsuario { get; set; }
        public int? IdExpediente { get; set; }
        public int? IdRegimenFiscal { get; set; }
        public string NumeroLicencia { get; set; }
        public int DiasPago { get; set; }
        public int? IdTipoCliente { get; set; }
        public int? IdListaPrecio { get; set; }
        public int? IdSatFormaPago { get; set; }
        public int? IdMetodoPago { get; set; }
        public bool? AdministradorCompania { get; set; }

        public List<int> IdsRol { get; set; }
        public List<int> IdsCompania { get; set; }
        public string Contrasena { get; set; }
    }
}
