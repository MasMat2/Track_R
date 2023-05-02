using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Cita
    {
        public Cita()
        {
            CitaArchivo = new HashSet<CitaArchivo>();
            CitaGrupoPersona = new HashSet<CitaGrupoPersona>();
            GrupoActividadCita = new HashSet<GrupoActividadCita>();
            OrdenImagenologia = new HashSet<OrdenImagenologia>();
            OrdenLaboratorio = new HashSet<OrdenLaboratorio>();
            Receta = new HashSet<Receta>();
        }

        public int IdCita { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public bool Subsecuente { get; set; }
        public string? Observaciones { get; set; }
        public int IdExpediente { get; set; }
        public int IdMotivoVisita { get; set; }
        public int IdUsuarioDoctor { get; set; }
        public int IdEstatusCita { get; set; }
        public int IdEstatusPago { get; set; }
        public int? IdArea { get; set; }
        public DateTime FechaCita { get; set; }
        public string? EvaluacionMedica { get; set; }
        public string? Diagnostico { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdRecibo { get; set; }
        public int IdHospital { get; set; }
        public bool Cancelada { get; set; }
        public DateTime? FechaTomaSomatometria { get; set; }
        public int? IdCie { get; set; }
        public int? IdNotaVenta { get; set; }
        public string? Referencia { get; set; }
        public string? OtroMotivoVisita { get; set; }
        public int? IdUsuarioTomaSomatometria { get; set; }

        public virtual Area? IdAreaNavigation { get; set; }
        public virtual Cie? IdCieNavigation { get; set; }
        public virtual EstatusCita IdEstatusCitaNavigation { get; set; } = null!;
        public virtual EstatusPago IdEstatusPagoNavigation { get; set; } = null!;
        public virtual Expediente IdExpedienteNavigation { get; set; } = null!;
        public virtual Hospital IdHospitalNavigation { get; set; } = null!;
        public virtual MotivoVisita IdMotivoVisitaNavigation { get; set; } = null!;
        public virtual NotaVenta? IdNotaVentaNavigation { get; set; }
        public virtual Presentacion? IdPresentacionNavigation { get; set; }
        public virtual Recibo? IdReciboNavigation { get; set; }
        public virtual Usuario IdUsuarioDoctorNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioTomaSomatometriaNavigation { get; set; }
        public virtual ICollection<CitaArchivo> CitaArchivo { get; set; }
        public virtual ICollection<CitaGrupoPersona> CitaGrupoPersona { get; set; }
        public virtual ICollection<GrupoActividadCita> GrupoActividadCita { get; set; }
        public virtual ICollection<OrdenImagenologia> OrdenImagenologia { get; set; }
        public virtual ICollection<OrdenLaboratorio> OrdenLaboratorio { get; set; }
        public virtual ICollection<Receta> Receta { get; set; }
    }
}
