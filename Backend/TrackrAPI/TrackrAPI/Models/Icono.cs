﻿using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Icono
    {
        public Icono()
        {
            Acceso = new HashSet<Acceso>();
        }

        public int IdIcono { get; set; }
        public string Clase { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Acceso> Acceso { get; set; }
    }
}
