using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class AyudaSeccion
    {
        public AyudaSeccion()
        {
            AccesoAyuda = new HashSet<AccesoAyuda>();
        }

        public int IdAyudaSeccion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int Orden { get; set; }

        public virtual ICollection<AccesoAyuda> AccesoAyuda { get; set; }
    }
}
