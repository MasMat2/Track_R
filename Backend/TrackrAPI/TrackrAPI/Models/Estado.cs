using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Compania = new HashSet<Compania>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }

        public virtual Pais IdPaisNavigation { get; set; } = null!;
        public virtual ICollection<Compania> Compania { get; set; }
    }
}
