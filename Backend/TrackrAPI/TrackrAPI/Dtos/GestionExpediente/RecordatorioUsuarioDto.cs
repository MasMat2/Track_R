namespace TrackrAPI.Dtos.GestionExpediente;

public class RecordatorioUsuarioDto
{
    public int Padecimiento { get; set; }  = 0;
    public string Indicaciones { get; set; } = string.Empty;
    public byte Dia { get; set; }
    public List<TomaDto> Tomas { get; set; } = new List<TomaDto>();
}
