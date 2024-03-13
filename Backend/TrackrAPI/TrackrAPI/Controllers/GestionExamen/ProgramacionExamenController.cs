using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Services.GestionExamen;
using TrackrAPI.Services.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Helpers;

namespace TrackrAPI.Controllers.GestionExamen;

[Route("api/[controller]")]
[ApiController]
public class ProgramacionExamenController : ControllerBase
{
    private readonly ProgramacionExamenService _programacionExamenService;
    private readonly UsuarioService _usuarioService;

    public ProgramacionExamenController(ProgramacionExamenService programacionExamenService, UsuarioService usuarioService)
    {
        _programacionExamenService = programacionExamenService;
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [Route("consultarTodosParaSelector")]
    public IEnumerable<ProgramacionExamenGridDto> ConsultarTodosParaSelector()
    {
        return _programacionExamenService.ConsultarTodosParaSelector();
    }

    [HttpGet]
    [Route("consultarGeneral")]
    public IEnumerable<ProgramacionExamenGridDto> ConsultarGeneral()
    {
        var idUsuarioSesion = Utileria.ObtenerIdUsuarioSesion(this);
        var usuario = _usuarioService.Consultar(idUsuarioSesion);

        if (usuario == null) {
            throw new CdisException("El usuario no existe");
        }

        return _programacionExamenService.ConsultarGeneral((int)usuario.IdCompania!);
    }

    [HttpGet]
    [Route("consultar/{idProgramacionExamen}")]
    public ProgramacionExamenDto? Consultar(int idProgramacionExamen)
    {
        return _programacionExamenService.Consultar(idProgramacionExamen);
    }

    [HttpPost]
    [Route("agregar")]
    public int Agregar(ProgramacionExamen programacionExamen)
    {
        return _programacionExamenService.Agregar(programacionExamen);
    }

    [HttpPut]
    [Route("editar")]
    public void Editar(ProgramacionExamen programacionExamen)
    {
        _programacionExamenService.Editar(programacionExamen);
    }

    [HttpDelete]
    [Route("eliminar/{idProgramacionExamen}")]
    public void Eliminar(int idProgramacionExamen)
    {
        _programacionExamenService.Eliminar(idProgramacionExamen);
    }
}
