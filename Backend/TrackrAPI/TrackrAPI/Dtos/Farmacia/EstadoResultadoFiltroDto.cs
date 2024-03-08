using System;

namespace TrackrAPI.Dtos.Pdfs
{
    public class EstadoResultadoFiltroDto
    {
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdLocacion { get; set; }

    }
}
