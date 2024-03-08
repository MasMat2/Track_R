using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Contabilidad
{
    public class ArchivoExcelDto
    {
        public string ArchivoBase64 { get; set; }
        public string ArchivoNombre { get; set; }
        public string ArchivoTipoMime { get; set; }
    }
}
