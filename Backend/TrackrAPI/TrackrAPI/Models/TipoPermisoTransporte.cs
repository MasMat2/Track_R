using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoPermisoTransporte
    {
        public TipoPermisoTransporte()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdTipoPermisoTransporte { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
