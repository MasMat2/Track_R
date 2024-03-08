using TrackrAPI.Dtos.Notificaciones;

namespace TrackrAPI.Hubs;

public interface ISignalingHub
{
    Task LocalId(string local_id);
    Task CalleeConnected(string local_id);
    Task NewMessage(Message json_string);
    
}

public class Message
{
    public string Id { get; set; }
    public string Content { get; set; }
}