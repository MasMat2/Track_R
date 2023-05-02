using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Flujo
    {
        public Flujo()
        {
            ConfiguracionOpcionVenta = new HashSet<ConfiguracionOpcionVenta>();
            FlujoDetalle = new HashSet<FlujoDetalle>();
            GuiaActividad = new HashSet<GuiaActividad>();
            Presentacion = new HashSet<Presentacion>();
            ProyectoActividad = new HashSet<ProyectoActividad>();
        }

        public int IdFlujo { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdCompania { get; set; }
        public bool? EsDefault { get; set; }
        public int? IdTipoFlujo { get; set; }
        public int? IdRol { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual Rol? IdRolNavigation { get; set; }
        public virtual TipoFlujo? IdTipoFlujoNavigation { get; set; }
        public virtual ICollection<ConfiguracionOpcionVenta> ConfiguracionOpcionVenta { get; set; }
        public virtual ICollection<FlujoDetalle> FlujoDetalle { get; set; }
        public virtual ICollection<GuiaActividad> GuiaActividad { get; set; }
        public virtual ICollection<Presentacion> Presentacion { get; set; }
        public virtual ICollection<ProyectoActividad> ProyectoActividad { get; set; }
    }
}
