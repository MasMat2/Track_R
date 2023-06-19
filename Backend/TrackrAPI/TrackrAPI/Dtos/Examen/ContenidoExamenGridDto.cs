﻿namespace TrackrAPI.Dtos.Examen;

public class ContenidoExamenGridDto
{
    public int IdContenidoExamen { get; set; }
    public string Asignatura { get; set; } = null!;
    public string NivelExamen { get; set; } = null!;
    public string Clave { get; set; } = null!;
    public int? TotalPreguntas { get; set; }
    public double? Duracion { get; set; }
}
