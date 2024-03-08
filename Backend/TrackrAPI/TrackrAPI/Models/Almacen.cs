using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Almacen
    {
        public Almacen()
        {
            HospitalIdAlmacenCaducoNavigation = new HashSet<Hospital>();
            HospitalIdAlmacenProduccionNavigation = new HashSet<Hospital>();
            InventarioFisico = new HashSet<InventarioFisico>();
            Kardex = new HashSet<Kardex>();
            MovimientoMaterial = new HashSet<MovimientoMaterial>();
            OrdenCompra = new HashSet<OrdenCompra>();
            PuntoVenta = new HashSet<PuntoVenta>();
            TraspasoMovimientoMaterialIdAlmacenDestinoNavigation = new HashSet<TraspasoMovimientoMaterial>();
            TraspasoMovimientoMaterialIdAlmacenOrigenNavigation = new HashSet<TraspasoMovimientoMaterial>();
            Ubicacion = new HashSet<Ubicacion>();
            UsuarioAlmacen = new HashSet<UsuarioAlmacen>();
        }

        public int IdAlmacen { get; set; }
        public string Numero { get; set; } = null!;
        public string? Nombre { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public string Calle { get; set; } = null!;
        public string NumeroExterior { get; set; } = null!;
        public string? NumeroInterior { get; set; }
        public string Colonia { get; set; } = null!;
        public string Localidad { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public string TelefonoUno { get; set; } = null!;
        public string TelefonoDos { get; set; } = null!;
        public int IdEstatusAlmacen { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public int IdEstado { get; set; }
        public int IdCompania { get; set; }
        public string? Clave { get; set; }
        public int? IdCuentaContable { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual CuentaContable? IdCuentaContableNavigation { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual EstatusAlmacen IdEstatusAlmacenNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioResponsableNavigation { get; set; } = null!;
        public virtual ICollection<Hospital> HospitalIdAlmacenCaducoNavigation { get; set; }
        public virtual ICollection<Hospital> HospitalIdAlmacenProduccionNavigation { get; set; }
        public virtual ICollection<InventarioFisico> InventarioFisico { get; set; }
        public virtual ICollection<Kardex> Kardex { get; set; }
        public virtual ICollection<MovimientoMaterial> MovimientoMaterial { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
        public virtual ICollection<PuntoVenta> PuntoVenta { get; set; }
        public virtual ICollection<TraspasoMovimientoMaterial> TraspasoMovimientoMaterialIdAlmacenDestinoNavigation { get; set; }
        public virtual ICollection<TraspasoMovimientoMaterial> TraspasoMovimientoMaterialIdAlmacenOrigenNavigation { get; set; }
        public virtual ICollection<Ubicacion> Ubicacion { get; set; }
        public virtual ICollection<UsuarioAlmacen> UsuarioAlmacen { get; set; }
    }
}
