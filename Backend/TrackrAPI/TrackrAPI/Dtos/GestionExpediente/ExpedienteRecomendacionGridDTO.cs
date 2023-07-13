namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteRecomendacionGridDTO
    {
        public int IdExpedienteRecomendacion { get; set; }
        public string? Fecha { get; set; }
        public string? Recomendacion { get; set; }
        public string? Doctor { get; set; }
        public int IdDoctor { get; set; }
    }
}
