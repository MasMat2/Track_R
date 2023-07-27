using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.GestionCaja
{
    public class CajaGridDto
    {
        public int IdCaja { get; set; }
        public string Nombre { get; set; }
        public int IdHotel { get; set; }
        public string NombreHotel { get; set; }
        public string Descripcion { get; set; }
        public string NombreTipoActivo { get; set; }

    }
}
