namespace TrackrAPI.Dtos.Padecimientos;

public class PadecimientoVariablesDTO
{
    public int IdEntidadEstructura { get; set; }
    public int? IdPadecimiento { get; set; }
    public string NombrePadecimiento { get; set; }
    public int IdEntidad { get; set; }
    public string DescripcionWidget { get; set; }
    public int? IdWidgetEntidad { get; set; }
    public string IconoEntidad { get; set; }
    public int IdSeccion { get; set; }
    public string NombreSeccion { get; set; }
    public string SeccionClave { get; set; }
    public List<VariableDTO> Variables { get; set; }
}