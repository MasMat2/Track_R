using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Caja
    {
        public Caja()
        {
            CajaTurno = new HashSet<CajaTurno>();
            MovimientoEstadoCuenta = new HashSet<MovimientoEstadoCuenta>();
            NotaFlujo = new HashSet<NotaFlujo>();
            Pago = new HashSet<Pago>();
            Recibo = new HashSet<Recibo>();
        }

        public int IdCaja { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdHospital { get; set; }
        public string? Descripcion { get; set; }
        public bool Automatica { get; set; }
        public int? IdUsuarioResponsable { get; set; }
        public int? IdTipoActivo { get; set; }
        public int? IdCuentaContable { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdCuentaContableAutomatica { get; set; }

        public virtual CuentaContable? IdCuentaContableAutomaticaNavigation { get; set; }
        public virtual CuentaContable? IdCuentaContableNavigation { get; set; }
        public virtual Hospital IdHospitalNavigation { get; set; } = null!;
        public virtual Moneda? IdMonedaNavigation { get; set; }
        public virtual TipoActivo? IdTipoActivoNavigation { get; set; }
        public virtual Usuario? IdUsuarioResponsableNavigation { get; set; }
        public virtual ICollection<CajaTurno> CajaTurno { get; set; }
        public virtual ICollection<MovimientoEstadoCuenta> MovimientoEstadoCuenta { get; set; }
        public virtual ICollection<NotaFlujo> NotaFlujo { get; set; }
        public virtual ICollection<Pago> Pago { get; set; }
        public virtual ICollection<Recibo> Recibo { get; set; }
    }
}
