using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Devolucion
    {
        public Devolucion()
        {
            DevolucionPresentacion = new HashSet<DevolucionPresentacion>();
        }

        public int IdDevolucion { get; set; }
        public string Folio { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public int IdLiquidacion { get; set; }
        public int IdTipoDevolucion { get; set; }
        public bool Habilitado { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public int IdDomicilioSucursal { get; set; }

        public virtual Domicilio IdDomicilioSucursalNavigation { get; set; } = null!;
        public virtual Liquidacion IdLiquidacionNavigation { get; set; } = null!;
        public virtual TipoDevolucion IdTipoDevolucionNavigation { get; set; } = null!;
        public virtual ICollection<DevolucionPresentacion> DevolucionPresentacion { get; set; }
    }
}
