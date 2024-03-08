using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Services.Chats;

namespace TrackrAPI.Controllers.Chats
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ChatMensajeVistoController : ControllerBase
    {
        private readonly ChatMensajeVistoService _chatMensajeVistoService;

        public ChatMensajeVistoController(ChatMensajeVistoService chatMensajeVistoService)
        {
            _chatMensajeVistoService = chatMensajeVistoService;
        }

        [HttpPost]
        private void agregarPersonaMensajeVisto(ChatMensajeVistoFormDTO chatMensajeVistoFormDTO)
        {
            _chatMensajeVistoService.agregarPersonaMensajeVisto(chatMensajeVistoFormDTO);
        }
    }
}
