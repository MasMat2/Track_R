using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Comision
    {
        public Comision()
        {
            NotaGastoDetalle = new HashSet<NotaGastoDetalle>();
        }

        public int IdComision { get; set; }
        public int IdUsuario { get; set; }
        public int? IdTipoComisionDetalle { get; set; }
        public int? IdNotaVentaDetalle { get; set; }
        public DateTime FechaAlta { get; set; }
        public decimal? MontoComision { get; set; }
        public bool Aplicada { get; set; }
        public bool? Pagada { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaContable { get; set; }
        public int? IdHospital { get; set; }

        public virtual Hospital? IdHospitalNavigation { get; set; }
        public virtual NotaVentaDetalle? IdNotaVentaDetalleNavigation { get; set; }
        public virtual TipoComisionDetalle? IdTipoComisionDetalleNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<NotaGastoDetalle> NotaGastoDetalle { get; set; }
    }
}
