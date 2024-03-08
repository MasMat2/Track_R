using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class DominioDetalle
    {
        public int IdDominioDetalle { get; set; }
        public string Valor { get; set; } = null!;
        public int IdDominio { get; set; }
        public bool Habilitado { get; set; }
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual Dominio IdDominioNavigation { get; set; } = null!;
    }
}
