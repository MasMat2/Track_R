using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente.ExpedienteDoctor;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.GestionExpediente;

[Route("api/[controller]")]
[ApiController]
public class ExpedienteDoctorController : ControllerBase
{

    private readonly ExpedienteDoctorService _expedienteDoctorService;

    public ExpedienteDoctorController(
        ExpedienteDoctorService expedienteDoctorService
    )
    {
        _expedienteDoctorService = expedienteDoctorService;

    }

    [HttpGet]
    public IEnumerable<ExpedienteDoctorCardsDTO> ConsultarExpediente()
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        return _expedienteDoctorService.ConsultarExpediente(idUsuario);
    }

    [HttpGet("conImagenes")]
    public IEnumerable<ExpedienteDoctorConImagenPerfilDTO> ConsultarExpedienteConImagenesPerfil()
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        return _expedienteDoctorService.ConsultarExpedienteConImagenesPerfil(idUsuario);
    }

    [HttpGet("selector")]
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarSelector()
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        return _expedienteDoctorService.ConsultarSelector(idUsuario);
    }

    [HttpGet("selectorPorUsuario")]
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarPorUsuarioParaSelector()
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        return _expedienteDoctorService.ConsultarPorUsuarioSelector(idUsuario);
    }

    [HttpDelete]
    public void Eliminar(ExpedienteDoctorDTO expedienteDoctorDTO)
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        expedienteDoctorDTO.IdUsuarioDoctor = idUsuario;
        _expedienteDoctorService.Eliminar(expedienteDoctorDTO);
    }
    [HttpPost]
    public void Agregar(ExpedienteDoctorDTO expedienteDoctorDTO)
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        _expedienteDoctorService.Agregar(expedienteDoctorDTO, idUsuario);
    }

}