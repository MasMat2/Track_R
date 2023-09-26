using TrackrAPI.Dtos.GestionExpediente;
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

    public IEnumerable<UsuarioPadecimientosDTO> Consultar(int idUsuario)
    {
        return _widgetRepository.ConsultarPorUsuario(idUsuario);
    } 

    public IEnumerable<TipoWidget> ConsultarTipo()
    {
        return _widgetRepository.ConsultarTipo();
    }
}