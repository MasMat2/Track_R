using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Liquidacion
    {
        public Liquidacion()
        {
            CobranzaPago = new HashSet<CobranzaPago>();
            Devolucion = new HashSet<Devolucion>();
            Gasto = new HashSet<Gasto>();
            Necesidad = new HashSet<Necesidad>();
            Recepcion = new HashSet<Recepcion>();
            Remision = new HashSet<Remision>();
        }

        public int IdLiquidacion { get; set; }
        public string Folio { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public int IdEstatusLiquidacion { get; set; }
        public bool Habilitado { get; set; }
        public DateTime? FechaLiquidacion { get; set; }
        public int IdCompania { get; set; }
        public int? IdMovimientoMaterialSalida { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual EstatusLiquidacion IdEstatusLiquidacionNavigation { get; set; } = null!;
        public virtual MovimientoMaterial? IdMovimientoMaterialSalidaNavigation { get; set; }
        public virtual ICollection<CobranzaPago> CobranzaPago { get; set; }
        public virtual ICollection<Devolucion> Devolucion { get; set; }
        public virtual ICollection<Gasto> Gasto { get; set; }
        public virtual ICollection<Necesidad> Necesidad { get; set; }
        public virtual ICollection<Recepcion> Recepcion { get; set; }
        public virtual ICollection<Remision> Remision { get; set; }
    }
}
