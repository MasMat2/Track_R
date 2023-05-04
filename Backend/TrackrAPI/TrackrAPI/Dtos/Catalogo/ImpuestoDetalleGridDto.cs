namespace TrackrAPI.Dtos.Catalogo
{
    public class ImpuestoDetalleGridDto
    {
        public int IdImpuestoDetalle { get; set; }
        public decimal Valor { get; set; }
        public string Descripcion { get; set; }
        public bool MovimientoContrario { get; set; }
        public bool Retencion { get; set; }
        public string CuentaCargoNombre { get; set; }
        public string CuentaAbonoNombre { get; set; }
        public string ClaveImpuestoSat { get; set; }
    }
}
