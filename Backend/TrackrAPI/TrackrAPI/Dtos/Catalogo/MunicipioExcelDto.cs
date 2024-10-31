namespace TrackrAPI.Dtos.Catalogo
{
    public class MunicipioExcelDto
    {
        public string CVE_ENT { get; set; } = string.Empty;
        public string CVE_MUN { get; set; } = string.Empty;
        public string NOM_MUN { get; set; } = string.Empty;
        public int? IdMunicipio { get; set; }
        public int? IdEstado { get; set; }
        public string? NombreEstado { get; set; }
    }
}