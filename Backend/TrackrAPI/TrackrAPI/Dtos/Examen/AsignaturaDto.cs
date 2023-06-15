namespace TrackrAPI.Dtos.Examen;

public class AsignaturaDto
{
    public int IdAsignatura { get; set; }
    public string Clave { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public DateTime? FechaAlta { get; set; }
    public bool? Estatus { get; set; }
}
