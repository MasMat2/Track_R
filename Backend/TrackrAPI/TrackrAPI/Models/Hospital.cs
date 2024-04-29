using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Hospital
    {
        public Hospital()
        {
            Artefacto = new HashSet<Artefacto>();
            Dominio = new HashSet<Dominio>();
            DominioHospital = new HashSet<DominioHospital>();
            HospitalLogotipo = new HashSet<HospitalLogotipo>();
            Proyecto = new HashSet<Proyecto>();
            Usuario = new HashSet<Usuario>();
            UsuarioLocacion = new HashSet<UsuarioLocacion>();
        }

        public int IdHospital { get; set; }
        public string? Nombre { get; set; }
        public string? Calle { get; set; }
        public string? NumeroExterior { get; set; }
        public string? NumeroInterior { get; set; }
        public string? Colonia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public DateTime FechaContableActual { get; set; }
        public int? IdUsuarioGerente { get; set; }
        public int? IdCompania { get; set; }
        public int? IdEstado { get; set; }
        public string? Ciudad { get; set; }
        public int? IdBanco { get; set; }
        public string? Cuenta { get; set; }
        public string? Clabe { get; set; }
        public string PortalWeb { get; set; } = null!;
        public string? Rfc { get; set; }
        public int? IdRegimenFiscal { get; set; }
        public int? IdLada { get; set; }
        public string? NombreComercial { get; set; }
        public int? IdMunicipio { get; set; }
        public string? EntreCalles { get; set; }
        public int? IdListaPrecioDefault { get; set; }
        public int? IdListaPrecioLinea { get; set; }
        public int? IdAlmacenProduccion { get; set; }
        public int? IdAlmacenCaduco { get; set; }
        public bool Predeterminada { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual Estado? IdEstadoNavigation { get; set; }
        public virtual Lada? IdLadaNavigation { get; set; }
        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual RegimenFiscal? IdRegimenFiscalNavigation { get; set; }
        public virtual Usuario? IdUsuarioGerenteNavigation { get; set; }
        public virtual ICollection<Artefacto> Artefacto { get; set; }
        public virtual ICollection<Dominio> Dominio { get; set; }
        public virtual ICollection<DominioHospital> DominioHospital { get; set; }
        public virtual ICollection<HospitalLogotipo> HospitalLogotipo { get; set; }
        public virtual ICollection<Proyecto> Proyecto { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<UsuarioLocacion> UsuarioLocacion { get; set; }
    }
}
