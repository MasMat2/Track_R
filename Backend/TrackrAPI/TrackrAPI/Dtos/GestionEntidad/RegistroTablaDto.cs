using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Dtos.GestionEntidad
{
    public class RegistroTablaDto
    {
        public int IdRegistroTabla { get; set; }
        public int IdEntidadEstructura { get; set; }
        public List<EntidadEstructuraTablaValor> Valores { get; set; }
    }
}
