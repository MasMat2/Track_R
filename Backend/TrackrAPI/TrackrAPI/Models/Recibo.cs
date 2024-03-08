using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Recibo
    {
        public Recibo()
        {
            Cita = new HashSet<Cita>();
            NotaVenta = new HashSet<NotaVenta>();
            OrdenImagenologia = new HashSet<OrdenImagenologia>();
            OrdenLaboratorio = new HashSet<OrdenLaboratorio>();
            ReciboDetalle = new HashSet<ReciboDetalle>();
            ReciboPago = new HashSet<ReciboPago>();
            Urgencia = new HashSet<Urgencia>();
        }

        public int IdRecibo { get; set; }
        public DateTime FechaAlta { get; set; }
        public int IdUsuarioAlta { get; set; }
        public string Folio { get; set; } = null!;
        public int IdArea { get; set; }
        public int? IdPaciente { get; set; }
        public int? IdCaja { get; set; }
        public int? IdUsuarioCaja { get; set; }
        public bool Habilitado { get; set; }
        public int? IdEstatusPago { get; set; }
        public int? IdUbicacionVenta { get; set; }
        public decimal? Total { get; set; }
        public decimal? Saldo { get; set; }
        public int? IdHospital { get; set; }
        public int? IdTipoRecibo { get; set; }
        public bool? EsPorcentajeDescuentoAdicional { get; set; }
        public string? MotivoDescuentoAdicional { get; set; }
        public decimal? DescuentoAdicional { get; set; }
        public decimal? ValorCapturadoDescuentoAdicional { get; set; }
        public DateTime? FechaContable { get; set; }
        public string? NumeroMovimientoBancario { get; set; }
        public int? IdUsuarioCliente { get; set; }

        public virtual Area IdAreaNavigation { get; set; } = null!;
        public virtual Caja? IdCajaNavigation { get; set; }
        public virtual EstatusPago? IdEstatusPagoNavigation { get; set; }
        public virtual Hospital? IdHospitalNavigation { get; set; }
        public virtual Paciente? IdPacienteNavigation { get; set; }
        public virtual TipoRecibo? IdTipoReciboNavigation { get; set; }
        public virtual UbicacionVenta? IdUbicacionVentaNavigation { get; set; }
        public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioCajaNavigation { get; set; }
        public virtual Usuario? IdUsuarioClienteNavigation { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
        public virtual ICollection<OrdenImagenologia> OrdenImagenologia { get; set; }
        public virtual ICollection<OrdenLaboratorio> OrdenLaboratorio { get; set; }
        public virtual ICollection<ReciboDetalle> ReciboDetalle { get; set; }
        public virtual ICollection<ReciboPago> ReciboPago { get; set; }
        public virtual ICollection<Urgencia> Urgencia { get; set; }
    }
}
