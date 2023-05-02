using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            MovimientoMaterial = new HashSet<MovimientoMaterial>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdDepartamento { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<MovimientoMaterial> MovimientoMaterial { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
