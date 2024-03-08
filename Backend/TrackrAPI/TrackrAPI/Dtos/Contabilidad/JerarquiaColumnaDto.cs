namespace TrackrAPI.Dtos.Contabilidad
{
    public class JerarquiaColumnaDto
    {
        public int IdJerarquiaColumna { get; set; }
        public string Nombre { get; set; }
        public string Letra { get; set; }
        public bool AgregadoPorSistema { get; set; }
        public int IdJerarquia { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public string Acumula { get; set; }
        public bool? EsPorcentaje { get; set; }
        public int? IdVersionPoliza { get; set; }
        public int? IdJerarquiaColumnaRelacionada { get; set; }

    }
}
