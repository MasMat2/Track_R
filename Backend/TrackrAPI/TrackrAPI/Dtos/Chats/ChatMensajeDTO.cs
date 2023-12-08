namespace TrackrAPI.Dtos.Chats;

public class ChatMensajeDTO
{
    public int IdChatMensaje {  get; set; }
    public int IdChat {  get; set; }
    public int IdPersona { get; set; }
    public string Mensaje { get; set; }
    public DateTime Fecha { get; set; }
    public string NombrePersona { get; set; }
}

