﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos
{
    public class TipoUsuarioDto
    {
        public int IdTipoUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }
}
