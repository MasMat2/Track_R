using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Padecimientos;
using TrackrAPI.Helpers;
using TrackrAPI.Services.Padecimientos;

namespace TrackrAPI.Controllers.Padecimientos;

[ApiController]
[Route("api/[controller]")]
public class PadecimientoController : ControllerBase
{
    private readonly PadecimientoService _padecimientoService;

    public PadecimientoController(PadecimientoService padecimientoService)
    {
        _padecimientoService = padecimientoService;
    }

    [HttpGet("pacientesPorPadecimiento")]
    public IEnumerable<PacientesPorPadecimientoDTO> ConsultarPacientesPorPadecimiento()
    {
        int idDoctor = Utileria.ObtenerIdUsuarioSesion(this);
        return _padecimientoService.ConsultarPacientesPorPadecimiento(idDoctor);
    }
}