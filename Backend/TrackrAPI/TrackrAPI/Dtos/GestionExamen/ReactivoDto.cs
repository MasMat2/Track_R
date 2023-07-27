namespace TrackrAPI.Dtos.GestionExamen;

public class ReactivoDto
{
    public int IdReactivo { get; set; }
    public int IdAsignatura { get; set; }
    public int IdNivelExamen { get; set; }
    public string Clave { get; set; } = null!;
    public string Pregunta { get; set; } = null!;
    public byte[] Imagen { get; set; } = null!;
    public string ImagenTipoMime { get; set; } = null!;
    public string ImagenNombre { get; set; } = null!;
    public string ImagenBase64 { get; set; } = null!;
    public string Respuesta { get; set; } = null!;
    public string RespuestaCorrecta { get; set; } = null!;
    public bool? NecesitaRevision { get; set; }
    public DateTime? FechaAlta { get; set; }
    public bool Estatus { get; set; }
}
