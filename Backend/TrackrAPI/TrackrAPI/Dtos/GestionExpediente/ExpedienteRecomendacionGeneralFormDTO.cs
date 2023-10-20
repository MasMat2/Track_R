namespace TrackrAPI.Dtos.GestionExpediente;
public class ExpedienteRecomendacionGeneralFormDTO
{
    public string? Descripcion { get; set; }
    public int IdDoctor { get; set; }
    public DateTime Fecha { get; set; }
    public int? IdPadecimiento { get; set; }
}
