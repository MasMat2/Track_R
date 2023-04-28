using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CodigoPostal
    {
        public int IdCodigoPostal { get; set; }
        public string CodigoPostal1 { get; set; } = null!;
        public string Colonia { get; set; } = null!;
        public int IdMunicipio { get; set; }

        public virtual Municipio IdMunicipioNavigation { get; set; } = null!;
    }
}
