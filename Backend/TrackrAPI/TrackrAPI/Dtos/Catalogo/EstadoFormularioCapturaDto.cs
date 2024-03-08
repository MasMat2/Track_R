namespace TrackrAPI.Dtos.Catalogo;

public class EstadoFormularioCapturaDto
{
    public int IdEstado { get; set; }
    public string Clave { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int IdPais { get; set; }
}
