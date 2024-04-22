using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Contabilidad
{
    public class PolizaDetalleGridDto
    {
        public int IdPolizaDetalle { get; set; }
        public string Concepto { get; set; }
        public decimal? Cargo { get; set; }
        public decimal? Abono { get; set; }
        public int Renglon { get; set; }
        public int IdCuentaContable { get; set; }
        public string CuentaContable { get; set; }
        public int? IdAuxiliar { get; set; }
        public string DescripcionAuxiliar { get; set; }
        public int IdPartidaViva { get; set; }
        public int? IdImpuestoDetalle { get; set; }
        public int? IdImpuesto { get; set; }
        public string DescripcionImpuesto { get; set; }
        public bool EstaViva { get; set; }
        public decimal? ValorImpuestos { get; set; }

        public PolizaDetalleGridDto()
        {
        }

        //public PolizaDetalleGridDto(PolizaDetalle polizaDetalle)
        //{
        //    IdImpuestoDetalle = polizaDetalle.IdImpuestoDetalle;
        //    IdImpuesto = polizaDetalle.IdImpuesto;
        //    IdCuentaContable = polizaDetalle.IdCuentaContable;
        //    IdPolizaDetalle = polizaDetalle.IdPolizaDetalle;
        //    IdAuxiliar = polizaDetalle.IdAuxiliar;
        //    Cargo = polizaDetalle.Cargo;
        //    Abono = polizaDetalle.Abono;
        //    Renglon = polizaDetalle.Renglon;
        //    Concepto = polizaDetalle.Concepto;
        //}
    }
}
