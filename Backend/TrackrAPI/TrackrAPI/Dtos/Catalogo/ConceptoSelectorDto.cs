using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class ConceptoSelectorDto
    {
        public int IdConcepto { get; set; }
        public string Nombre { get; set; }
        public int? IdCuentaContable { get; set; }
        public string NumeroCuentaContable { get; set; }
        public string SelectorLabel { get; set; }

    }
}
