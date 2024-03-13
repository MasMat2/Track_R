using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExcelPolizaCargaMasiva
    {
        public ExcelPolizaCargaMasiva()
        {
            ExcelPolizaCargaMasivaError = new HashSet<ExcelPolizaCargaMasivaError>();
        }

        public int IdExcelPolizaCargaMasiva { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public string Estatus { get; set; } = null!;
        public string TotalPolizasExitosas { get; set; } = null!;
        public string TotalPolizasFallidas { get; set; } = null!;
        public int? IdUsuarioAlta { get; set; }
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioAltaNavigation { get; set; }
        public virtual ICollection<ExcelPolizaCargaMasivaError> ExcelPolizaCargaMasivaError { get; set; }
    }
}
