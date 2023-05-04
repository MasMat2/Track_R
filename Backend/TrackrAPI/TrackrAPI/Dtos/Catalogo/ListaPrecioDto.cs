using System;
using System.Collections.Generic;

namespace TrackrAPI.Dtos.Catalogo
{
    public class ListaPrecioDto
    {
        public int IdListaPrecio { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public List<int> Clinica { get; set; }
        public string Observaciones { get; set; }
        public int? IdMoneda { get; set; }
        public bool? EsDefault { get; set; }
    }
}
