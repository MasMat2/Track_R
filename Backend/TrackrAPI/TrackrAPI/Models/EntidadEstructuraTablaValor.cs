﻿using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EntidadEstructuraTablaValor
    {
        public int IdEntidadEstructuraTablaValor { get; set; }
        public int Numero { get; set; }
        public int IdEntidadEstructura { get; set; }
        public string ClaveCampo { get; set; } = null!;
        public string? Valor { get; set; }
        public int? IdTabla { get; set; }

        public virtual EntidadEstructura IdEntidadEstructuraNavigation { get; set; } = null!;
    }
}
