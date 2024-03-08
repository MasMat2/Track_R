namespace TrackrAPI.Dtos.GestionExamen;

public class TipoExamenGridDto
{
    public int IdTipoExamen { get; set; }
    public string Clave { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public int? TotalPreguntas { get; set; }
    public double? Duracion { get; set; }
}
