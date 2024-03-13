using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoVehiculo
    {
        public TipoVehiculo()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdTipoVehiculo { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
