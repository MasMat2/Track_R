using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class FlujoDetalleAplicado
    {
        public FlujoDetalleAplicado()
        {
            FlujoDetalleAplicadoResponsable = new HashSet<FlujoDetalleAplicadoResponsable>();
            PedidoPresentacion = new HashSet<PedidoPresentacion>();
            ProyectoActividad = new HashSet<ProyectoActividad>();
        }

        public int IdFlujoDetalleAplicado { get; set; }
        public int IdFlujoDetalle { get; set; }
        public bool Aplicado { get; set; }
        public DateTime? FechaAplicacion { get; set; }
        public int? IdUsuarioAplicacion { get; set; }
        public string? Comentarios { get; set; }
        public string Origen { get; set; } = null!;
        public int IdOrigen { get; set; }

        public virtual FlujoDetalle IdFlujoDetalleNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioAplicacionNavigation { get; set; }
        public virtual ICollection<FlujoDetalleAplicadoResponsable> FlujoDetalleAplicadoResponsable { get; set; }
        public virtual ICollection<PedidoPresentacion> PedidoPresentacion { get; set; }
        public virtual ICollection<ProyectoActividad> ProyectoActividad { get; set; }
    }
}
