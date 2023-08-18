namespace TrackrAPI.Dtos.Notificaciones;

public class NotificacionUsuarioDto
{
    public int IdNotificacionUsuario { get; set; }
    public int IdNotificacion { get; set; }
    public int IdUsuario { get; set; }
    public DateTime FechaAlta { get; set; }
    public string Descripcion { get; set; } = null!;
    public string Origen { get; set; } = null!;
    public bool Visto { get; set; }
}