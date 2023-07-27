using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Hospital
    {
        public Hospital()
        {
            Artefacto = new HashSet<Artefacto>();
            BitacoraMovimientoUsuario = new HashSet<BitacoraMovimientoUsuario>();
            Caja = new HashSet<Caja>();
            CertificadoLocacion = new HashSet<CertificadoLocacion>();
            Cita = new HashSet<Cita>();
            Comision = new HashSet<Comision>();
            ComplementoPago = new HashSet<ComplementoPago>();
            EntradaPersonal = new HashSet<EntradaPersonal>();
            Factura = new HashSet<Factura>();
            HospitalLogotipo = new HashSet<HospitalLogotipo>();
            ListaPrecioClinica = new HashSet<ListaPrecioClinica>();
            NotaFlujo = new HashSet<NotaFlujo>();
            NotaGasto = new HashSet<NotaGasto>();
            PedidoPresentacion = new HashSet<PedidoPresentacion>();
            Proyecto = new HashSet<Proyecto>();
            Recibo = new HashSet<Recibo>();
            SatMovimiento = new HashSet<SatMovimiento>();
            SatSolicitud = new HashSet<SatSolicitud>();
            Urgencia = new HashSet<Urgencia>();
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

        public virtual Almacen? IdAlmacenCaducoNavigation { get; set; }
        public virtual Almacen? IdAlmacenProduccionNavigation { get; set; }
        public virtual Banco? IdBancoNavigation { get; set; }
        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual Estado? IdEstadoNavigation { get; set; }
        public virtual Lada? IdLadaNavigation { get; set; }
        public virtual ListaPrecio? IdListaPrecioDefaultNavigation { get; set; }
        public virtual ListaPrecio? IdListaPrecioLineaNavigation { get; set; }
        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual RegimenFiscal? IdRegimenFiscalNavigation { get; set; }
        public virtual Usuario? IdUsuarioGerenteNavigation { get; set; }
        public virtual ICollection<Artefacto> Artefacto { get; set; }
        public virtual ICollection<BitacoraMovimientoUsuario> BitacoraMovimientoUsuario { get; set; }
        public virtual ICollection<Caja> Caja { get; set; }
        public virtual ICollection<CertificadoLocacion> CertificadoLocacion { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<Comision> Comision { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPago { get; set; }
        public virtual ICollection<EntradaPersonal> EntradaPersonal { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<HospitalLogotipo> HospitalLogotipo { get; set; }
        public virtual ICollection<ListaPrecioClinica> ListaPrecioClinica { get; set; }
        public virtual ICollection<NotaFlujo> NotaFlujo { get; set; }
        public virtual ICollection<NotaGasto> NotaGasto { get; set; }
        public virtual ICollection<PedidoPresentacion> PedidoPresentacion { get; set; }
        public virtual ICollection<Proyecto> Proyecto { get; set; }
        public virtual ICollection<Recibo> Recibo { get; set; }
        public virtual ICollection<SatMovimiento> SatMovimiento { get; set; }
        public virtual ICollection<SatSolicitud> SatSolicitud { get; set; }
        public virtual ICollection<Urgencia> Urgencia { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<UsuarioLocacion> UsuarioLocacion { get; set; }
    }
}
