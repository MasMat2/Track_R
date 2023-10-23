using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
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
        _expedienteRecomendacionGeneralService.AgregarTodos(expedienteRecomendacionGeneralFormDTO);
    }

    [HttpPost("porPadecimiento")]
    public void AgregarPorPadecimiento(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionGeneralService.AgregarPorPadecimiento(expedienteRecomendacionGeneralFormDTO);
    }

    [HttpPost("porPaciente")]
    public void AgregarPorPaciente(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionGeneralService.AgregarPacientes(expedienteRecomendacionGeneralFormDTO);
    }
}

