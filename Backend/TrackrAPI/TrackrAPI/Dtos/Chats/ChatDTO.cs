namespace TrackrAPI.Models;
public partial class ChatDTO
{

    public int IdChat { get; set; }
    public DateTime Fecha { get; set; }
    public bool Habilitado { get; set; }
    public string? Titulo { get; set; }
    public string ImagenBase64 { get; set; }
    public string TipoMime { get; set; }

}

