namespace TrackrAPI.Dtos.GestionExamen;

public class ExamenGridDto
{
    public int IdExamen { get; set; }
    public string TipoExamen { get; set; } = null!;
    public DateTime? FechaExamen { get; set; }
    public TimeSpan? HoraExamen { get; set; }
    public double? Duracion { get; set; }
    public int? TotalPreguntas { get; set; }
}
