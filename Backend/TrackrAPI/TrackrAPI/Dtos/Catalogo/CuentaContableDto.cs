namespace TrackrAPI.Dtos.Catalogo
{
    public class CuentaContableDto
    {
        public int IdCuentaContable { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Reconciliatoria { get; set; }
        public bool RecibeMovimientos { get; set; }
        public bool Auxiliar { get; set; }
        public bool PartidaAbierta { get; set; }
        public bool Automatica { get; set; }
        public int? IdSubtipoCuentaContable { get; set; }
        public int? IdTipoCuentaContable { get; set; }
        public int? IdTipoAuxiliar { get; set; }
        public bool? EsConcepto { get; set; }

        public int? IdCuentaContablePadre { get; set; }


        public string NumeroNombre { get; set; }
        public int? IdAgrupadorCuentaContable { get; set; }

        public CuentaContableDto()
        {
        }
    }
}
