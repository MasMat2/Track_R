namespace TrackrAPI.Dtos.GestionExamen;

public class ExamenDto
{
    public int IdExamen { get; set; }
    public string TipoExamen { get; set; } = null!;
    public DateTime? FechaExamen { get; set; }
    public TimeSpan? HoraExamen { get; set; }
    public double? Duracion { get; set; }
    public string NombreUsuario { get; set; } = null!;
    public string Clave { get; set; } = null!;
    public int? TotalPreguntas { get; set; }
}
