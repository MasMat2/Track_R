using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExcelArchivo
    {
        public ExcelArchivo()
        {
            ExcelError = new HashSet<ExcelError>();
        }

        public int IdExcelArchivo { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaSubida { get; set; }
        public string Estatus { get; set; } = null!;
        public int? UsuarioSubida { get; set; }

        public virtual ICollection<ExcelError> ExcelError { get; set; }
    }
}
