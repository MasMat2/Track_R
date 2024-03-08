namespace TrackrAPI.Dtos.GestionExpediente;
public class ExpedienteRecomendacionGeneralFormDTO
{
    public int? IdExpedienteRecomendacionesGenerales {  get; set; }
    public int Tipo { get; set; }
    public string? Descripcion { get; set; }
    public int IdDoctor { get; set; }
    public DateTime Fecha { get; set; }
    public int? IdPadecimiento { get; set; }
    public List<int>? Paciente { get; set;}
}
