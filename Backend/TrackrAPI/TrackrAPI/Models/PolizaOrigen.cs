using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class PolizaOrigen
    {
        public int IdPolizaOrigen { get; set; }
        public int IdPoliza { get; set; }
        public int IdOrigen { get; set; }
        public string Origen { get; set; } = null!;

        public virtual Poliza IdPolizaNavigation { get; set; } = null!;
    }
}
