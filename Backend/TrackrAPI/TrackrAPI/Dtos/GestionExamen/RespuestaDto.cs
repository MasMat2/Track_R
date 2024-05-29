namespace TrackrAPI.Dtos.GestionExamen;
public class RespuestaDto
{
    public int IdReactivo { get; set; }
    public int IdRespuesta { get; set; }
    public string? Clave { get; set; }
    public string? Respuesta1 { get; set; }
    public bool? RespuestaCorrecta { get; set; }
    public int? Valor { get; set; }
}
