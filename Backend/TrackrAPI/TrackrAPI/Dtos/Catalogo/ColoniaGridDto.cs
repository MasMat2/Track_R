using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class ColoniaGridDto
    {
        public int IdColonia { get; set; }
        public string Clave { get; set; }
        public string CodigoPostal { get; set; }
        public string Nombre { get; set; }
    }
}
