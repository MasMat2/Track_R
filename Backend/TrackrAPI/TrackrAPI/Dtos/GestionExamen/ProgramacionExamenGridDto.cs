namespace TrackrAPI.Dtos.GestionExamen;

public class ProgramacionExamenGridDto
{
    public int IdProgramacionExamen { get; set; }
    public string Clave { get; set; } = null!;
    public string UsuarioResponsable { get; set; } = null!;
    public string TipoExamen { get; set; } = null!;
    public DateTime? FechaExamen { get; set; }
    public TimeSpan? HoraExamen { get; set; }
    public double? Duracion { get; set; }
    public int? CantidadParticipantes { get; set; }
    public bool? Estatus { get; set; }
}
