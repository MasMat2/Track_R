namespace TrackrAPI.Dtos.Examen;

public class TipoExamenDto
{
    public int IdTipoExamen { get; set; }
    public string Clave { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public int? TotalPreguntas { get; set; }
    public double? Duracion { get; set; }
    public DateTime? FechaAlta { get; set; }
    public bool? Estatus { get; set; }
}
