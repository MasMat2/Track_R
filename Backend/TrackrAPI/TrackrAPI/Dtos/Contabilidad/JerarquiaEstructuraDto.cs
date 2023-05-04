namespace TrackrAPI.Dtos.Contabilidad
{
    public class JerarquiaEstructuraDto
    {

        public JerarquiaEstructuraDto()
        {
        }

        public int IdJerarquiaEstructura { get; set; }
        public int? IdCuentaContable { get; set; }
        public int? IdAuxiliar { get; set; }
        public int? IdJerarquiaEstructuraPadre { get; set; }
        public string Descripcion { get; set; }
        public bool TieneMovimientos { get; set; }
        public string ClaveSaldoInicial { get; set; }
        public string ClaveCargo { get; set; }
        public string ClaveAbono { get; set; }
        public string ClaveSaldoFinal { get; set; }
        public int TotalHijos { get; set; }
        public decimal Cargo { get; set; }
        public decimal Abono { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }

        public string DescripcionCuenta
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.Descripcion = value;
                }
            }
        }

        public string AuxiliarCuenta
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.Descripcion = value;
                }
            }
        }

    }
}
