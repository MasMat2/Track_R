using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Padecimientos;
using TrackrAPI.Helpers;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Padecimientos;

namespace TrackrAPI.Controllers.Padecimientos;

[ApiController]
[Route("api/[controller]")]
public class PadecimientoController : ControllerBase
{
    private readonly PadecimientoService _padecimientoService;
    private readonly IUsuarioRepository _usuarioRepository;

    public PadecimientoController(PadecimientoService padecimientoService, IUsuarioRepository usuarioRepository)
    {
        _padecimientoService = padecimientoService;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet("pacientesPorPadecimiento")]
    public IEnumerable<PacientesPorPadecimientoDTO> ConsultarPacientesPorPadecimiento()
    {
        var doctor = _usuarioRepository.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
        return _padecimientoService.ConsultarPacientesPorPadecimiento(doctor.IdUsuario, doctor.IdCompania);
    }
}