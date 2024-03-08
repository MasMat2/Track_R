using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.GestionEgresos
{
    public class TipoAuxiliarDto
    {
        public int IdTipoAuxiliar { get; set; }
        public string Descripcion { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }
}
