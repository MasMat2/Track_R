namespace TrackrAPI.Dtos.GestionExamen;

public class ProgramacionExamenDto
{
    public int IdProgramacionExamen { get; set; }
    public int IdTipoExamen { get; set; }
    public int IdUsuarioResponsable { get; set; }
    public int IdProyectoElementoTecnica { get; set; }
    public string Clave { get; set; } = null!;
    public double? Duracion { get; set; }
    public int? CantidadParticipantes { get; set; }
    public DateTime? FechaExamen { get; set; }
    public TimeSpan? HoraExamen { get; set; }
    public DateTime? FechaAlta { get; set; }
    public bool? Estatus { get; set; }
    public double? Promedio { get; set; }
}
