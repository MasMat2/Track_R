using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NotaVenta
    {
        public NotaVenta()
        {
            Cita = new HashSet<Cita>();
            InverseIdNotaVentaPadreDevolucionNavigation = new HashSet<NotaVenta>();
            NotaVentaDetalleIdNotaVentaNavigation = new HashSet<NotaVentaDetalle>();
            NotaVentaDetalleIdNotaVentaPadreDevolucionNavigation = new HashSet<NotaVentaDetalle>();
        }

        public int IdNotaVenta { get; set; }
        public DateTime FechaAlta { get; set; }
        public int IdUsuarioAlta { get; set; }
        public int IdRecibo { get; set; }
        public int? IdMovimientoMaterial { get; set; }
        public int? IdPuntoVenta { get; set; }
        public int? IdPaciente { get; set; }
        public int? IdUbicacionVenta { get; set; }
        public string? Identificador { get; set; }
        public string? Numero { get; set; }
        public decimal? Total { get; set; }
        public int? IdTipoNotaVenta { get; set; }
        public int? IdNotaVentaPadreDevolucion { get; set; }
        public int? IdEstatusNotaVenta { get; set; }
        public string? Observaciones { get; set; }
        public int? IdConcepto { get; set; }
        public int? IdUsuarioCliente { get; set; }
        public DateTime? FechaContable { get; set; }
        public int? IdPedido { get; set; }
        public bool? Facturada { get; set; }
        public int? IdFactura { get; set; }
        public int? IdExpedienteAdministrativo { get; set; }

        public virtual Concepto? IdConceptoNavigation { get; set; }
        public virtual EstatusNotaVenta? IdEstatusNotaVentaNavigation { get; set; }
        public virtual ExpedienteAdministrativo? IdExpedienteAdministrativoNavigation { get; set; }
        public virtual Factura? IdFacturaNavigation { get; set; }
        public virtual MovimientoMaterial? IdMovimientoMaterialNavigation { get; set; }
        public virtual NotaVenta? IdNotaVentaPadreDevolucionNavigation { get; set; }
        public virtual Paciente? IdPacienteNavigation { get; set; }
        public virtual Pedido? IdPedidoNavigation { get; set; }
        public virtual PuntoVenta? IdPuntoVentaNavigation { get; set; }
        public virtual Recibo IdReciboNavigation { get; set; } = null!;
        public virtual TipoNotaVenta? IdTipoNotaVentaNavigation { get; set; }
        public virtual UbicacionVenta? IdUbicacionVentaNavigation { get; set; }
        public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioClienteNavigation { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<NotaVenta> InverseIdNotaVentaPadreDevolucionNavigation { get; set; }
        public virtual ICollection<NotaVentaDetalle> NotaVentaDetalleIdNotaVentaNavigation { get; set; }
        public virtual ICollection<NotaVentaDetalle> NotaVentaDetalleIdNotaVentaPadreDevolucionNavigation { get; set; }
    }
}
