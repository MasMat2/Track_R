using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CajaTurno
    {
        public CajaTurno()
        {
            CajaTurnoDetalle = new HashSet<CajaTurnoDetalle>();
            Pago = new HashSet<Pago>();
        }

        public int IdCajaTurno { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal? FondoCaja { get; set; }
        public DateTime FechaAlta { get; set; }
        public decimal? MontoEsperado { get; set; }
        public decimal? MontoIngresado { get; set; }
        public bool TurnoCerrado { get; set; }
        public DateTime FechaContable { get; set; }
        public int IdCaja { get; set; }
        public int? IdTurno { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdCierre { get; set; }
        public decimal Importe { get; set; }

        public virtual Caja IdCajaNavigation { get; set; } = null!;
        public virtual Cierre? IdCierreNavigation { get; set; }
        public virtual Turno? IdTurnoNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<CajaTurnoDetalle> CajaTurnoDetalle { get; set; }
        public virtual ICollection<Pago> Pago { get; set; }
    }
}
