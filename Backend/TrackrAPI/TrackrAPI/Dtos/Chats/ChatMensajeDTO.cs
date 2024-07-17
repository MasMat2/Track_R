namespace TrackrAPI.Dtos.Chats;

public class ChatMensajeDTO
{
    public int IdChatMensaje {  get; set; }
    public int IdChat {  get; set; }
    public int IdPersona { get; set; }
    public string Mensaje { get; set; }
    public DateTime Fecha { get; set; }
    public string NombrePersona { get; set; }
    public int IdArchivo { get; set; }
    public string ? Nombre { get; set; }
    public DateTime ? FechaRealizacion { get; set; }
    public string ? Archivo { get; set; }
    public string ? ArchivoTipoMime { get; set; }
    public string ? ArchivoNombre { get; set; }
    public bool ? EsVideoChat { get; set; }   
}

