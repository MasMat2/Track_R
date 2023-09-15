using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Dashboard;

namespace TrackrAPI.Services.Dashboard;

public class WidgetService
{
    private readonly IWidgetRepository _widgetRepository;

    public WidgetService(IWidgetRepository widgetRepository)
    {
        _widgetRepository = widgetRepository;
    }

    public IEnumerable<WidgetDto> Consultar()
    {
        return _widgetRepository.Consultar()
            .Select(w => new WidgetDto(
                IdWidget: w.IdWidget,
                Clave: w.Clave,
                Nombre: w.Nombre
            ));
    }

    public IEnumerable<TipoWidget> ConsultarTipo()
    {
        return _widgetRepository.ConsultarTipo();
    }
}