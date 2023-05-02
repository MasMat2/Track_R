using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ScriptCambio
    {
        public int IdScriptCambio { get; set; }
        public string Script { get; set; } = null!;
        public DateTime FechaEjecucion { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string ResultadoEjecucion { get; set; } = null!;
    }
}
