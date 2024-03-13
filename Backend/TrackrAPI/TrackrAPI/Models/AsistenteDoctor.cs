using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class AsistenteDoctor
    {
        public int IdAsistenteDoctor { get; set; }
        public int? IdDoctor { get; set; }
        public int? IdAsistente { get; set; }

        public virtual Usuario? IdAsistenteNavigation { get; set; }
        public virtual Usuario? IdDoctorNavigation { get; set; }
    }
}
