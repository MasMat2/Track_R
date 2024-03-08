using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class VehiculoMantenimiento
    {
        public int IdVehiculoMantenimiento { get; set; }
        public int Kilometraje { get; set; }
        public DateTime FechaMantenimiento { get; set; }
        public string? Descripcion { get; set; }
        public int IdVehiculo { get; set; }

        public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
    }
}
