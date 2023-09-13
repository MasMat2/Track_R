using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestablecerContrasenaController : ControllerBase
    {
        private readonly RestablecerContrasenaService _restablecerContrasenaService;

        public RestablecerContrasenaController(RestablecerContrasenaService restablecerContrasenaService)
        {
            _restablecerContrasenaService = restablecerContrasenaService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("restablecerContrasena")]
        public void RestablecerContrasena(RestablecerContrasenaDto usuario)
        {
            _restablecerContrasenaService.RestablecerContrasena(usuario);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("validarActualizarContrasena")]
        public bool ValidarActualizarContrasena(RestablecerContrasenaDto usuario)
        {
            return _restablecerContrasenaService.ValidarActualizarContrasena(usuario);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("procesarActualizacionContrasena")]
        public void ProcesarActualizacionContrasena(RestablecerContrasenaDto usuario)
        {
            _restablecerContrasenaService.ProcesarActualizacionContrasena(usuario);
        }

    }
}
