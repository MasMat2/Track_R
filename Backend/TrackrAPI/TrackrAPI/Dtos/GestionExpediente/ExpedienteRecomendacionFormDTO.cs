namespace TrackrAPI.Dtos.GestionExpediente;

public class ExpedienteRecomendacionFormDTO
{

    public int IdExpedienteRecomendacion { get; set; }
    public string? Descripcion { get; set; }
    public int IdUsuario { get; set; }
    public int IdDoctor { get; set; }
    public DateTime Fecha { get; set; }

}
