using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoMuestra
    {
        public TipoMuestra()
        {
            EstudioLaboratorioMuestra = new HashSet<EstudioLaboratorioMuestra>();
        }

        public int IdTipoMuestra { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<EstudioLaboratorioMuestra> EstudioLaboratorioMuestra { get; set; }
    }
}
