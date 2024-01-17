using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Helpers;
using TrackrAPI.Services.GestionExpediente;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.GestionExpediente;

[Route("api/[controller]")]
[ApiController]
public class ExpedienteTrackrController : ControllerBase
{
    private readonly ExpedienteTrackrService _expedienteTrackrService;
    private readonly UsuarioService _usuarioService;

    public ExpedienteTrackrController(ExpedienteTrackrService expedienteTrackrService, UsuarioService usuarioService)
    {
        this._expedienteTrackrService = expedienteTrackrService;
        this._usuarioService = usuarioService;
    }

    [HttpGet]
    [Route("consultarWrapperPorUsuario/{idUsuario}")]
    public ExpedienteWrapper ConsultarWrapperPorUsuario(int idUsuario)
    {
        return _expedienteTrackrService.ConsultarWrapperPorUsuario(idUsuario);
    }

    [HttpPost("agregarWrapper")]
    public int AgregarWrapper(ExpedienteWrapper expedienteWrapper)
    {
        return _expedienteTrackrService.AgregarWrapper(expedienteWrapper);
    }

    [HttpPost("editarWrapper")]
    public int EditarWrapper(ExpedienteWrapper expedienteWrapper)
    {
        return _expedienteTrackrService.EditarWrapper(expedienteWrapper);
    }

    [HttpGet("consultarParaGrid/")]
    public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaGrid()
    {
        var usuario  =  _usuarioService.ConsultarDto(Utileria.ObtenerIdUsuarioSesion(this));
        return _expedienteTrackrService.ConsultarParaGrid(usuario.IdUsuario , usuario.IdCompania);
    }

    [HttpDelete("eliminar/{idExpediente}")]
    public void Eliminar(int idExpediente)
    {
        _expedienteTrackrService.Eliminar(idExpediente);
    }
    [HttpGet("sidebar/{idUsuario}")]
    public UsuarioExpedienteSidebarDTO ConsultarParaSidebar(int idUsuario)
    {
        return _expedienteTrackrService.ConsultarParaSidebar(idUsuario);
    }

    [HttpGet("apegoMedicamentoUsuarios")]
    public IEnumerable<ApegoTomaMedicamentoDto> ApegoMedicamentoUsuarios()
    {
        int idDoctor = Utileria.ObtenerIdUsuarioSesion(this);
        return _expedienteTrackrService.ApegoMedicamentoUsuarios(idDoctor);
    }

}
