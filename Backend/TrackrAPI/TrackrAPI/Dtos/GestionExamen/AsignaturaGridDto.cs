﻿namespace TrackrAPI.Dtos.GestionExamen;

public class AsignaturaGridDto
{
    public int IdAsignatura { get; set; }
    public string Clave { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
}
