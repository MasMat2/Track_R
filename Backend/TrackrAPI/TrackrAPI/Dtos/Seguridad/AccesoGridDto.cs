using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Dtos.Seguridad
{
    public class AccesoGridDto
    {
        public int IdAcceso { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Url { get; set; }
        public string TipoAcceso { get; set; }
        public string AccesoPadre { get; set; }
        public int? OrdenMenu { get; set; }
        public int? IdRolAcceso { get; set; }
        public int? IdAccesoPadre { get; set; }
        public List<AccesoGridDto> Hijos { get; set; }
        public int? CantidadDescendientes { get; set; }

    }
}
