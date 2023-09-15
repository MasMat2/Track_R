using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Area
    {
        public Area()
        {
            Cita = new HashSet<Cita>();
            Departamento = new HashSet<Departamento>();
            FlujoDetalle = new HashSet<FlujoDetalle>();
            Pedido = new HashSet<Pedido>();
            PedidoPresentacion = new HashSet<PedidoPresentacion>();
            Presentacion = new HashSet<Presentacion>();
            Recibo = new HashSet<Recibo>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdArea { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<Departamento> Departamento { get; set; }
        public virtual ICollection<FlujoDetalle> FlujoDetalle { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual ICollection<PedidoPresentacion> PedidoPresentacion { get; set; }
        public virtual ICollection<Presentacion> Presentacion { get; set; }
        public virtual ICollection<Recibo> Recibo { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
