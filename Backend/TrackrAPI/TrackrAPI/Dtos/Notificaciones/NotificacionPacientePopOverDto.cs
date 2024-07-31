namespace TrackrAPI.Dtos.Notificaciones;

public class NotificacionPacientePopOverDto{
    public int IdNotificacion {get; set;}
    public int IdTipoNotificacion { get; set; }
    public string Titulo {get; set; }
    public string Mensaje {get; set; }
    public string? ComplementoMensaje { get; set; }
    public DateTime FechaAlta {get; set; }
    public bool Visto{get; set; }
}