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
    private readonly WidgetService _widgetService;

    public UsuarioWidgetController(UsuarioWidgetService usuarioWidgetService, WidgetService widgetService)
    {
        _usuarioWidgetService = usuarioWidgetService;
        _widgetService = widgetService;
    }

    [HttpGet("usuario")]
    public IEnumerable<string> ConsultarPorUsuarioEnSesion()
    {
        int idUsuarioSesion = Utileria.ObtenerIdUsuarioSesion(this);
        return _usuarioWidgetService.ConsultarPorUsuario(idUsuarioSesion);
    }

    [HttpPut("modificar")]
    public void ModificarWidgets(string[] widgets)
    {
        int idUsuarioSesion = Utileria.ObtenerIdUsuarioSesion(this);

        _usuarioWidgetService.modificarSeleccionWidgets(idUsuarioSesion, widgets);

    }

    
    [HttpGet("widget-types")]
    public IEnumerable<string> ConsultarClaves()
    {
        return _widgetService.ConsultarClaves();
    }
}