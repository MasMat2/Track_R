namespace TrackrAPI.Dtos.GestionExpediente;

public class ExpedienteRecomendacionDTO
{
    public int IdExpedienteRecomendacion { get; set; }
    public int IdExpediente { get; set; }
    public DateTime Fecha { get; set; }
    public string? Descripcion { get; set; } = string.Empty;
    public int IdDoctor { get; set; }
}
