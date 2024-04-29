using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class RegimenFiscal
    {
        public RegimenFiscal()
        {
            Compania = new HashSet<Compania>();
            Hospital = new HashSet<Hospital>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdRegimenFiscal { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
