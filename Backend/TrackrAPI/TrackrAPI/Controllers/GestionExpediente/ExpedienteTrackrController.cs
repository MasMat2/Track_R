using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Helpers;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.GestionExpediente;

[Route("api/[controller]")]
[ApiController]
public class ExpedienteTrackrController : ControllerBase
{
    private readonly ExpedienteTrackrService _expedienteTrackrService;

    public ExpedienteTrackrController(ExpedienteTrackrService expedienteTrackrService)
    {
        this._expedienteTrackrService = expedienteTrackrService;
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
        int idDoctor = Utileria.ObtenerIdUsuarioSesion(this);
        return _expedienteTrackrService.ConsultarParaGrid(idDoctor);
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
    [HttpGet("recordatoriosPorPadecimiento/{idUsuario}")]
    public IEnumerable<object> RecordatoriosPorPadecimiento(int idUsuario)
    {
        return _expedienteTrackrService.RecordatoriosPorPadecimiento(idUsuario);
    }
    [HttpGet("recordatoriosPorDia/{idUsuario}")]
    public IEnumerable<object> RecordatoriosPorDia(int idUsuario)
    {
        return _expedienteTrackrService.RecordatoriosPorDia(idUsuario);
    }

    [HttpGet("recordatoriosPorPadecimientoHoy/{idUsuario}")]
    public IEnumerable<object> RecordatoriosPorPadecimientoHoy(int idUsuario){
        return _expedienteTrackrService.RecordatoriosPorPadecimientoHoy(idUsuario);
    }

}
