using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Tarjeta
    {
        public int IdTarjeta { get; set; }
        public int IdUsuarioComprador { get; set; }
        public string OpenpayIdTarjeta { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string TerminacionNumero { get; set; } = null!;
        public string Titular { get; set; } = null!;
        public string AnioVencimiento { get; set; } = null!;
        public string MesVencimiento { get; set; } = null!;
        public bool? Usada { get; set; }

        public virtual Usuario IdUsuarioCompradorNavigation { get; set; } = null!;
    }
}
