using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Necesidad
    {
        public Necesidad()
        {
            NecesidadPresentacion = new HashSet<NecesidadPresentacion>();
        }

        public int IdNecesidad { get; set; }
        public string Folio { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public DateTime FechaAlta { get; set; }
        public int? IdLiquidacion { get; set; }
        public bool Habilitado { get; set; }
        public int? IdDomicilioSucursal { get; set; }
        public int IdUsuarioEmpleado { get; set; }

        public virtual Domicilio? IdDomicilioSucursalNavigation { get; set; }
        public virtual Liquidacion? IdLiquidacionNavigation { get; set; }
        public virtual Usuario IdUsuarioEmpleadoNavigation { get; set; } = null!;
        public virtual ICollection<NecesidadPresentacion> NecesidadPresentacion { get; set; }
    }
}
