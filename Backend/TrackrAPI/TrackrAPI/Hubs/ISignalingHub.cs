using TrackrAPI.Dtos.Notificaciones;

namespace TrackrAPI.Hubs;

public interface ISignalingHub
{
    Task LocalId(string local_id);
    Task PeerId(string local_id);
    Task NewMessage(String json_string);
    
}