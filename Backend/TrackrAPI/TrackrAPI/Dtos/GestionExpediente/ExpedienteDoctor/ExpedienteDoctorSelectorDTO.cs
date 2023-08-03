namespace TrackrAPI.Dtos.GestionExpediente.ExpedienteDoctor;

public class ExpedienteDoctorSelectorDTO
{
    public int IdUsuarioDoctor { get; set; }
    public int IdRol { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Ambito { get; set; } = string.Empty;

}