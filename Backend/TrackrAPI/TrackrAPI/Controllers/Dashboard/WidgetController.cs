using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Dashboard;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.Dashboard;

[ApiController]
[Route("api/[controller]")]
public class WidgetController : ControllerBase
{
    private readonly WidgetService _widgetService;

    public WidgetController(WidgetService widgetService)
    {
        _widgetService = widgetService;
    }

    [HttpGet]
    public IEnumerable<UsuarioPadecimientosDTO> WidgetsPorUsuario()
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        return _widgetService.ConsultarWidgetsSeguimiento(idUsuario);

    }

    [HttpGet("todos")]
    public IEnumerable<Widget> ConsultarWidgets()
    {
        return _widgetService.consultarTodos();

    }

    [HttpGet("tipo")]
    public IEnumerable<TipoWidget> ConsultarTipo()
    {
        return _widgetService.ConsultarTipo();
    }


    [HttpGet("configuracionDashboard/widgets")]
    public IEnumerable<Widget> ConsultarWidgetsConfiguracionDashboardPorUsuario()
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        return _widgetService.WidgetsDisponiblesPorUsuario(idUsuario);
    }
}