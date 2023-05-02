using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExcelError
    {
        public int IdExcelError { get; set; }
        public string ErrorMensaje { get; set; } = null!;
        public string? Fila { get; set; }
        public string Libro { get; set; } = null!;
        public int? IdExcelArchivo { get; set; }

        public virtual ExcelArchivo? IdExcelArchivoNavigation { get; set; }
    }
}
