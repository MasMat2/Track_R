using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteTratamiento
    {
        public ExpedienteTratamiento()
        {
            TratamientoRecordatorio = new HashSet<TratamientoRecordatorio>();
        }

        public int IdExpedienteTratamiento { get; set; }
        public int IdExpediente { get; set; }
        public string Farmaco { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; } = null!;
        public string Indicaciones { get; set; } = null!;
        public int IdPadecimiento { get; set; }
        public int IdUsuarioDoctor { get; set; }
        public byte[]? Imagen { get; set; }
        public string? ImagenTipoMime { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? ArchivoUrl { get; set; }

        public virtual ExpedienteTrackr IdExpedienteNavigation { get; set; } = null!;
        public virtual EntidadEstructura IdPadecimientoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioDoctorNavigation { get; set; } = null!;
        public virtual ICollection<TratamientoRecordatorio> TratamientoRecordatorio { get; set; }
    }
}
