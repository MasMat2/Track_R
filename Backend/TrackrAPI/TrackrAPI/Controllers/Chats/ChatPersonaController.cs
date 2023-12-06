using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Services.Chats;

namespace TrackrAPI.Controllers.Chats
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ChatPersonaController : ControllerBase
    {
        private readonly ChatPersonaService _chatPersonaService;

        public ChatPersonaController(ChatPersonaService chatPersonaService)
        {
            _chatPersonaService = chatPersonaService;
        }

        [HttpPost]
        public void agregarPersonaChat (ChatPersonaFormDTO chatPersonaFormDTO)
        {
            _chatPersonaService.agregarPersonaChat(chatPersonaFormDTO);
        }
    }
}
