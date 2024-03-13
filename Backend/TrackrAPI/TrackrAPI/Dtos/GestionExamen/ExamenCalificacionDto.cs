﻿namespace TrackrAPI.Dtos.GestionExamen;

public class ExamenCalificacionDto
{
    public int IdExamen { get; set; }
    public string NombreUsuario { get; set; } = null!;
    public int IdProgramacionExamen { get; set; }
    public int IdUsuarioParticipante { get; set; }
    public double? Resultado { get; set; }
    public int? PreguntasCorrectas { get; set; }
}
