namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteEstudioGridDTO
    {
        public int IdExpedienteEstudio { get; set; }
        public int IdExpediente { get; set; }
        public DateTime? FechaRealizacion { get; set; }
        public string? Nombre { get; set; }
        public string? UrlArchivo { get; set; }
        public int? IdArchivo { get; set; }
        public string? ArchivoTipoMime { get; set; }

    }
}
