using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Models;
using TrackrAPI.Services.Dashboard;

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
    public IActionResult Get()
    {
        var widgets = _widgetService.Consultar();
        return Ok(widgets);
    }

    [HttpGet("tipo")]
    public IEnumerable<TipoWidget> ConsultarTipo()
    {
        return _widgetService.ConsultarTipo();
    }
}