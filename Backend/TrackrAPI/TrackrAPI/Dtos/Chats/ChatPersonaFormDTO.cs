namespace TrackrAPI.Dtos.Chats;

public class ChatPersonaFormDTO
{
    public int IdChat {  get; set; }
    public List<int> IdPersonas { get; set; }
    public int IdTipo { get; set; }
}

