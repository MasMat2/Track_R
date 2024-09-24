namespace TrackrAPI.Dtos.GestionExamen;
public class RespuestasClasificacionPreguntaGridDto
{
    public int IdRespuestasClasificacionPregunta { get; set; }
    public int? IdClasificacionPregunta { get; set; }
    public string? Nombre { get; set; }
    public bool? Estatus { get; set; }
    public string? Identificador { get; set; }
    public int? Valor { get; set; }

}
