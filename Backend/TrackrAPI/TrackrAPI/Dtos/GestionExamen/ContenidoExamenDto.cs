namespace TrackrAPI.Dtos.GestionExamen;

public class ContenidoExamenDto
{
    public int IdContenidoExamen { get; set; }
    public int IdTipoExamen { get; set; }
    public int IdAsignatura { get; set; }
    public int IdNivelExamen { get; set; }
    public string Clave { get; set; } = null!;
    public int? TotalPreguntas { get; set; }
    public double? Duracion { get; set; }
    public DateTime? FechaAlta { get; set; }
    public bool? Estatus { get; set; }
}
