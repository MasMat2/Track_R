namespace TrackrAPI.Dtos.GestionEntidad
{
    public class EntidadEstructuraValorDto
    {
        public int IdEntidadEstructuraValor { get; set; }
        public int IdEntidadEstructura { get; set; }
        public string ClaveCampo { get; set; }
        public string Valor { get; set; }
        public int? IdTabla { get; set; }
    }
}