
namespace TrackrAPI.Dtos.GestionExpediente;

public class UsuarioExpedienteSidebarDTO
{
    public int IdUsuario { get; set; }
    public string NombreCompleto { get; set; }
    public string? TipoMime { get; set; }
    public string? ImagenBase64 { get; set; }
    public string Genero { get; set; }
    public string Edad { get; set; }
    public string? Colonia { get; set; }
    public string? Ciudad { get; set; }
    public string? Estado { get; set; }
    public IEnumerable<ExpedienteSidebarDTO>? Padecimientos { get; set; }
}

