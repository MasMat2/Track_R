using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmacionCorreoController: ControllerBase
    {
        private ConfirmacionCorreoService _confirmacionCorreoService;

        public ConfirmacionCorreoController(ConfirmacionCorreoService confirmacionCorreoService)
        {
            this._confirmacionCorreoService = confirmacionCorreoService;
        }

        [AllowAnonymous]
        [HttpPost("validarCorreo")]
        public bool ValidarCorreo(ConfirmarCorreoDto datos)
        {
            return this._confirmacionCorreoService.ValidarConfirmarCorreo(datos);
        }

        [HttpPost("enviarCorreoConfirmacion")]
        public void EnviarCorreoConfirmacion(ConfirmarCorreoDto correoUsuario)
        {
            this._confirmacionCorreoService.ConfirmarCorreo(correoUsuario.Correo);
        }

    }
}
