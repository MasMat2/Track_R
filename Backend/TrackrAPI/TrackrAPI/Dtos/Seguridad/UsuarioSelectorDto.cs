using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Seguridad
{
    public class UsuarioSelectorDto
    {
        public int IdUsuario { get; set; }
        public string RFC { get; set; }
        public string NombreCompleto { get; set; }
        public string SelectorLabel { get; set; }
    }
}
