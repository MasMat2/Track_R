using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Dashboard;

namespace TrackrAPI.Controllers.Dashboard;

[Route("api/[controller]")]
[ApiController]
public class UsuarioWidgetController : ControllerBase
{
    private readonly UsuarioWidgetService _usuarioWidgetService;

    public UsuarioWidgetController(UsuarioWidgetService usuarioWidgetService)
    {
        _usuarioWidgetService = usuarioWidgetService;
    }

    [HttpGet("usuario")]
    public IEnumerable<string> ConsultarPorUsuarioEnSesion()
    {
        int idUsuarioSesion = Utileria.ObtenerIdUsuarioSesion(this);
        return _usuarioWidgetService.ConsultarPorUsuario(idUsuarioSesion);
    }
}