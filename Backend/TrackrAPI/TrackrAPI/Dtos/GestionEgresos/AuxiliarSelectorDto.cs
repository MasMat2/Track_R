using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.GestionEgresos
{
    public class AuxiliarSelectorDto
    {
        public int IdAuxiliar { get; set; }
        public string Descripcion { get; set; }
        public string SelectorLabel { get; set; }
        public string Numero { get; set; }
    }
}
