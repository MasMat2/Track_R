using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExcelPolizaCargaMasivaError
    {
        public int IdExcelPolizaCargaMasivaError { get; set; }
        public string MensajeError { get; set; } = null!;
        public string? Renglon { get; set; }
        public string Hoja { get; set; } = null!;
        public int? IdExcelPolizaCargaMasiva { get; set; }

        public virtual ExcelPolizaCargaMasiva? IdExcelPolizaCargaMasivaNavigation { get; set; }
    }
}
