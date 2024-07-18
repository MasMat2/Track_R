using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Helpers;
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
            int IdUsuario = Utileria.TryObtenerIdUsuarioSesion(this);
            _chatPersonaService.agregarPersonaChat(chatPersonaFormDTO , IdUsuario);
        }

        [HttpGet("IdUsuario")]
        public int obtenerIdUsuario()
        {
            return Utileria.TryObtenerIdUsuarioSesion(this);
        }

        [HttpGet("Padecimiento/{IdPadecimiento}")]
        public List<int> ObtenerPacientesPorPadecimienot(int IdPadecimiento)
        {
            return _chatPersonaService.ObtenerPacientesPorPadecimiento(IdPadecimiento);
        }

        [HttpGet("PersonasEnChat/{IdChat}")]
        public async Task<List<ChatPersonaSelectorDTO>> ObtenerPersonasEnChatSelector(int IdChat)
        {
            return await _chatPersonaService.ObtenerPersonasChatSelector(IdChat);
        }
    }
}
