namespace TrackrAPI.Dtos.GestionExamen;

public class ExamenReactivoDto
{
    public int IdExamenReactivo { get; set; }
    public int IdReactivo { get; set; }
    public int IdExamen { get; set; }
    public string Asignatura { get; set; } = null!;
    public string Clave { get; set; } = null!;
    public string Pregunta { get; set; } = null!;
    public string ImagenBase64 { get; set; } = null!;
    public bool? Resultado { get; set; }
    public string Respuesta { get; set; } = null!;
    public string RespuestaAlumno { get; set; } = null!;
    public bool? NecesitaRevision { get; set; }
    public DateTime? FechaAlta { get; set; }
    public bool? Estatus { get; set; }
}
