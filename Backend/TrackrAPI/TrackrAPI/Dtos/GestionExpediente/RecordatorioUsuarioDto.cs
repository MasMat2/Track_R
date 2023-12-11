
public class RecordatorioUsuarioDto
{
    public int Padecimiento { get; set; }  = 0;
    public DateTime? FechaToma { get; set; }
    public DateTime? FechaEnvio { get; set; }
    public string Indicaciones { get; set; } = string.Empty;
    public int Dia { get; set; }
    public bool Tomado { get; set; }
}
