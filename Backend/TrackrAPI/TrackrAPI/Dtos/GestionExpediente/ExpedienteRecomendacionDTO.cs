namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteRecomendacionDTO
    {
        public int ExpedienteId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Recomendacion { get; set; } = string.Empty;
        public int DoctorId { get; set; }
    }
}
