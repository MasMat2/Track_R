using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Seguridad;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class RestablecerContrasenaController : ControllerBase
{
    private readonly RestablecerContrasenaService _restablecerContrasenaService;

    public RestablecerContrasenaController(RestablecerContrasenaService restablecerContrasenaService)
    {
        _restablecerContrasenaService = restablecerContrasenaService;
    }

    [HttpPost("restablecerContrasena")]
    public void RestablecerContrasena(RestablecerContrasenaDto usuario)
    {
        _restablecerContrasenaService.RestablecerContrasena(usuario);
    }

    [HttpPost("validarActualizarContrasena")]
    public bool ValidarActualizarContrasena(RestablecerContrasenaDto usuario)
    {
        return _restablecerContrasenaService.ValidarActualizarContrasena(usuario);
    }

    [HttpPost("procesarActualizacionContrasena")]
    public void ProcesarActualizacionContrasena(RestablecerContrasenaDto usuario)
    {
        _restablecerContrasenaService.ProcesarActualizacionContrasena(usuario);
    }

}
