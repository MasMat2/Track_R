using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Direccion
    {
        public int IdDireccion { get; set; }
        public string Calle { get; set; } = null!;
        public string Recibe { get; set; } = null!;
        public string? NumeroInterior { get; set; }
        public string NumeroExterior { get; set; } = null!;
        public string Colonia { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public string EntreCalles { get; set; } = null!;
        public string OtraReferencia { get; set; } = null!;
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdUsuarioComprador { get; set; }
        public string? Telefono { get; set; }

        public virtual Municipio? IdCiudadNavigation { get; set; }
    }
}
