using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstadoProducto
    {
        public EstadoProducto()
        {
            Recepcion = new HashSet<Recepcion>();
        }

        public int IdEstadoProducto { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Recepcion> Recepcion { get; set; }
    }
}
