namespace TrackrAPI.Dtos.Contabilidad
{
    public class JerarquiaConfiguracionDto
    {
        public int IdJerarquiaConfiguracion { get; set; }
        public string Clave { get; set; }
        public string Formula { get; set; }
        public bool AgregadoPorSistema { get; set; }
        public int? IdJerarquiaConfiguracionRelacionado { get; set; }
        public int IdJerarquiaEstructura { get; set; }
        public int IdJerarquiaColumna { get; set; }

        public int? IdJerarquiaRelacionada { get; set; }
        public int? IdJerarquiaEstructuraRelacionado { get; set; }
        public string NombreColumna { get; set; }
        public string CuentaRelacionada { get; set; }
        public string ColumnaRelacionada { get; set; }

    }
}
