using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class CuentaContableFiltroDto
    {
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public int? IdTipoCuentaContable { get; set; }
    }
}
