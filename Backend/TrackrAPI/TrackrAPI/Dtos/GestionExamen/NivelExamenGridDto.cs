﻿namespace TrackrAPI.Dtos.GestionExamen;

public class NivelExamenGridDto
{
    public int IdNivelExamen { get; set; }
    public string Clave { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public DateTime? FechaAlta { get; set; }
    public bool? Estatus { get; set; }
}
