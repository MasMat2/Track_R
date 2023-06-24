namespace TrackrAPI.Dtos.Catalogo;

public class MunicipioFormularioCapturaDto
{
    public int IdMunicipio { get; set; }
    public int IdEstado { get; set; }
    public string Clave { get; set; } = null!;
    public string Nombre { get; set; } = null!;
}