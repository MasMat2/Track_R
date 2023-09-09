using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Domicilio
    {
        public Domicilio()
        {
            Devolucion = new HashSet<Devolucion>();
            ExpedienteAdministrativoMercancia = new HashSet<ExpedienteAdministrativoMercancia>();
            ExpedienteAdministrativoViaje = new HashSet<ExpedienteAdministrativoViaje>();
            Necesidad = new HashSet<Necesidad>();
            OrdenCompra = new HashSet<OrdenCompra>();
            Pedido = new HashSet<Pedido>();
            Remision = new HashSet<Remision>();
        }

        public int IdDomicilio { get; set; }
        public string? Calle { get; set; }
        public string? NumeroExterior { get; set; }
        public string? NumeroInterior { get; set; }
        public string? Colonia { get; set; }
        public string? Localidad { get; set; }
        public string? CodigoPostal { get; set; }
        public int IdEstado { get; set; }
        public int? IdCompania { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdColonia { get; set; }
        public int? IdLocalidad { get; set; }
        public int? IdPais { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreSucursal { get; set; }
        public int? IdUsuarioRepartidor { get; set; }
        public int? IdMetodoPago { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? EntreCalles { get; set; }
        public string? OtraReferencia { get; set; }

        public virtual Colonia? IdColoniaNavigation { get; set; }
        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual Localidad? IdLocalidadNavigation { get; set; }
        public virtual MetodoPago? IdMetodoPagoNavigation { get; set; }
        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual Pais? IdPaisNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual Usuario? IdUsuarioRepartidorNavigation { get; set; }
        public virtual ICollection<Devolucion> Devolucion { get; set; }
        public virtual ICollection<ExpedienteAdministrativoMercancia> ExpedienteAdministrativoMercancia { get; set; }
        public virtual ICollection<ExpedienteAdministrativoViaje> ExpedienteAdministrativoViaje { get; set; }
        public virtual ICollection<Necesidad> Necesidad { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual ICollection<Remision> Remision { get; set; }
        public bool Activo { get; internal set; }
    }
}
