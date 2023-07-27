using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Poliza
    {
        public Poliza()
        {
            InverseIdPolizaReversaNavigation = new HashSet<Poliza>();
            PolizaAplicada = new HashSet<PolizaAplicada>();
            PolizaDetalle = new HashSet<PolizaDetalle>();
            PolizaOrigen = new HashSet<PolizaOrigen>();
        }

        public int IdPoliza { get; set; }
        public string Numero { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaContable { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string? Identificador { get; set; }
        public decimal? TipoCambio { get; set; }
        public bool EsReversa { get; set; }
        public int IdTipoPoliza { get; set; }
        public int IdCompania { get; set; }
        public int IdMoneda { get; set; }
        public int? IdPolizaReversa { get; set; }
        public bool EsPresupuesto { get; set; }
        public int? IdVersionPoliza { get; set; }
        public string? Origen { get; set; }
        public int? IdOrigen { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual Moneda IdMonedaNavigation { get; set; } = null!;
        public virtual Poliza? IdPolizaReversaNavigation { get; set; }
        public virtual TipoPoliza IdTipoPolizaNavigation { get; set; } = null!;
        public virtual VersionPoliza? IdVersionPolizaNavigation { get; set; }
        public virtual ICollection<Poliza> InverseIdPolizaReversaNavigation { get; set; }
        public virtual ICollection<PolizaAplicada> PolizaAplicada { get; set; }
        public virtual ICollection<PolizaDetalle> PolizaDetalle { get; set; }
        public virtual ICollection<PolizaOrigen> PolizaOrigen { get; set; }
    }
}
