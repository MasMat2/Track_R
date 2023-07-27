using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class OrdenImagenologia
    {
        public OrdenImagenologia()
        {
            EstudioImagenologia = new HashSet<EstudioImagenologia>();
        }

        public int IdOrdenImagenologia { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public int IdCita { get; set; }
        public int IdPresentacionEstudio { get; set; }
        public decimal PrecioEstudio { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public int IdInstitucionEstudio { get; set; }
        public string? Indicaciones { get; set; }
        public bool EsPacienteInterno { get; set; }
        public string ProcedenciaPaciente { get; set; } = null!;
        public string? FirmaCoordinadorClinica { get; set; }
        public int? IdRecibo { get; set; }

        public virtual Cita IdCitaNavigation { get; set; } = null!;
        public virtual Institucion IdInstitucionEstudioNavigation { get; set; } = null!;
        public virtual Presentacion IdPresentacionEstudioNavigation { get; set; } = null!;
        public virtual Recibo? IdReciboNavigation { get; set; }
        public virtual ICollection<EstudioImagenologia> EstudioImagenologia { get; set; }
    }
}
