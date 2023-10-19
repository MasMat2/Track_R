namespace TrackrAPI.Dtos.Padecimientos;
public class VariableDTO
{
    public string VariableClave { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool? MostrarDashboard { get; set; }
    public string IconoClase { get; set; } = string.Empty;
    public string ValorVariable { get; set; }

}