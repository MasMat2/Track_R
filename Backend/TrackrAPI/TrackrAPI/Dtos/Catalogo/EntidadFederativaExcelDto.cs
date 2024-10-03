namespace TrackrAPI.Dtos.Catalogo
{
    public class EntidadFederativaExcelDto
    {
        public string CATALOG_KEY { get; set; } = string.Empty;
        public string ENTIDAD_FEDERATIVA { get; set; } = string.Empty;
        public string ABREVIATURA { get; set; } = string.Empty;
        public int? IdEstado { get; set; }
    }
}