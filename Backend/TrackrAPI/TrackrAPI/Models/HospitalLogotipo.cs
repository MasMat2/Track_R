using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class HospitalLogotipo
    {
        public int IdHospitalLogotipo { get; set; }
        public int IdHospital { get; set; }
        public string Imagen { get; set; } = null!;
        public string TipoMime { get; set; } = null!;

        public virtual Hospital IdHospitalNavigation { get; set; } = null!;
    }
}
