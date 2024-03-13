namespace TrackrAPI.Dtos.GestionExamen;

public class ReactivoGridDto
{
    public int IdReactivo { get; set; }
    public string Asignatura { get; set; } = null!;
    public string NivelExamen { get; set; } = null!;
    public string Clave { get; set; } = null!;
    public string Pregunta { get; set; } = null!;
    public string Respuesta { get; set; } = null!;
    public string RespuestaCorrecta { get; set; } = null!;
}
