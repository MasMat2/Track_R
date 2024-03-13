namespace TrackrAPI.Dtos.Catalogo
{
    public class ImpuestoDetalleDto
    {
        public int IdImpuestoDetalle { get; set; }
        public decimal Valor { get; set; }
        public string Descripcion { get; set; }
        public bool Retencion { get; set; }
        public bool MovimientoContrario { get; set; }
        public int IdImpuesto { get; set; }
        public int IdTipoImpuesto { get; set; }
        public int? IdCuentaContableCargo { get; set; }
        public int? IdCuentaContableAbono { get; set; }

        public string ClaveImpuestoSat { get; set; }

    }
}
