using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstudioLaboratorio
    {
        public EstudioLaboratorio()
        {
            EstudioLaboratorioArchivo = new HashSet<EstudioLaboratorioArchivo>();
            EstudioLaboratorioMuestra = new HashSet<EstudioLaboratorioMuestra>();
        }

        public int IdEstudioLaboratorio { get; set; }
        public string? Observaciones { get; set; }
        public int? IdEstatusEstudioLaboratorio { get; set; }
        public int? IdOrdenLaboratorio { get; set; }

        public virtual EstatusEstudioLaboratorio? IdEstatusEstudioLaboratorioNavigation { get; set; }
        public virtual OrdenLaboratorio? IdOrdenLaboratorioNavigation { get; set; }
        public virtual ICollection<EstudioLaboratorioArchivo> EstudioLaboratorioArchivo { get; set; }
        public virtual ICollection<EstudioLaboratorioMuestra> EstudioLaboratorioMuestra { get; set; }
    }
}
