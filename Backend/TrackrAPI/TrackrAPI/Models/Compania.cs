using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Compania
    {
        public Compania()
        {
            Area = new HashSet<Area>();
            CompaniaContacto = new HashSet<CompaniaContacto>();
            CompaniaLogotipo = new HashSet<CompaniaLogotipo>();
            Departamento = new HashSet<Departamento>();
            Domicilio = new HashSet<Domicilio>();
            DominioDetalle = new HashSet<DominioDetalle>();
            Hospital = new HashSet<Hospital>();
            Jerarquia = new HashSet<Jerarquia>();
            JerarquiaAcceso = new HashSet<JerarquiaAcceso>();
            Perfil = new HashSet<Perfil>();
            Rol = new HashSet<Rol>();
            UnidadMedida = new HashSet<UnidadMedida>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdCompania { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Correo { get; set; }
        public string? PortalWeb { get; set; }
        public string? Calle { get; set; }
        public string? NumeroExterior { get; set; }
        public string? NumeroInterior { get; set; }
        public string? Colonia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Telefono { get; set; }
        public string? Ciudad { get; set; }
        public int? IdEstado { get; set; }
        public string? Rfc { get; set; }
        public int? IdLada { get; set; }
        public int? IdRegimenFiscal { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdAgrupadorCuentaContable { get; set; }
        public int? IdTipoCompania { get; set; }
        public bool? AfectacionContable { get; set; }
        public int? IdGiroComercial { get; set; }
        public bool? Timbrado { get; set; }
        public int? IdMunicipio { get; set; }
        public bool? UsoAlmacen { get; set; }

        public virtual Estado? IdEstadoNavigation { get; set; }
        public virtual GiroComercial? IdGiroComercialNavigation { get; set; }
        public virtual Lada? IdLadaNavigation { get; set; }
        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual RegimenFiscal? IdRegimenFiscalNavigation { get; set; }
        public virtual TipoCompania? IdTipoCompaniaNavigation { get; set; }
        public virtual ICollection<Area> Area { get; set; }
        public virtual ICollection<CompaniaContacto> CompaniaContacto { get; set; }
        public virtual ICollection<CompaniaLogotipo> CompaniaLogotipo { get; set; }
        public virtual ICollection<Departamento> Departamento { get; set; }
        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual ICollection<DominioDetalle> DominioDetalle { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
        public virtual ICollection<Jerarquia> Jerarquia { get; set; }
        public virtual ICollection<JerarquiaAcceso> JerarquiaAcceso { get; set; }
        public virtual ICollection<Perfil> Perfil { get; set; }
        public virtual ICollection<Rol> Rol { get; set; }
        public virtual ICollection<UnidadMedida> UnidadMedida { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
