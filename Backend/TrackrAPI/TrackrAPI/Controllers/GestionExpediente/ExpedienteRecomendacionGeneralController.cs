using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.GestionExpediente;

[Route("api/[controller]")]
[ApiController]
public class ExpedienteRecomendacionGeneralController : ControllerBase
{
    private readonly ExpedienteRecomendacionGeneralService _expedienteRecomendacionGeneralService;

    public ExpedienteRecomendacionGeneralController(
        ExpedienteRecomendacionGeneralService expedienteRecomendacionGeneralService)
    {
        _expedienteRecomendacionGeneralService = expedienteRecomendacionGeneralService;
    }

    [HttpPost]
    public void AgregarTodos(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        int IdDoctor = Utileria.ObtenerIdUsuarioSesion(this);
        expedienteRecomendacionGeneralFormDTO.IdDoctor = IdDoctor;
        _expedienteRecomendacionGeneralService.AgregarTodos(expedienteRecomendacionGeneralFormDTO);
    }

    [HttpPost("porPadecimiento")]
    public void AgregarPorPadecimiento(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        int IdDoctor = Utileria.ObtenerIdUsuarioSesion(this);
        expedienteRecomendacionGeneralFormDTO.IdDoctor = IdDoctor;
        _expedienteRecomendacionGeneralService.AgregarPorPadecimiento(expedienteRecomendacionGeneralFormDTO);
    }

    [HttpPost("porPaciente")]
    public void AgregarPorPaciente(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        int IdDoctor = Utileria.ObtenerIdUsuarioSesion(this);
        expedienteRecomendacionGeneralFormDTO.IdDoctor = IdDoctor;
        _expedienteRecomendacionGeneralService.AgregarPacientes(expedienteRecomendacionGeneralFormDTO);
    }

    [HttpGet]
    public IEnumerable<ExpedienteRecomendacionGeneralGridDTO> ConsultarGrid()
    {
        return _expedienteRecomendacionGeneralService.obtenerGrid();
        
    }
    
    [HttpPut]
    public void EditarRecomendacionGeneral(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionGeneralService.Editar(expedienteRecomendacionGeneralFormDTO);
    }

    [HttpGet("{idExpedienteRecomendacionesGenerales}")]
    public ExpedienteRecomendacionGeneralFormDTO ConsultarExpediente(int idExpedienteRecomendacionesGenerales)
    {
        return _expedienteRecomendacionGeneralService.Consultar(idExpedienteRecomendacionesGenerales);
    }

    [HttpDelete("{idExpedienteRecomendacionesGenerales}")]
    public void Eliminar(int idExpedienteRecomendacionesGenerales)
    {
        _expedienteRecomendacionGeneralService.Eliminar(idExpedienteRecomendacionesGenerales);
    }

}

