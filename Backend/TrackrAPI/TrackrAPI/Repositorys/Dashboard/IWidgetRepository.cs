using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard;

public interface IWidgetRepository : IRepository<Widget>
{
    public IEnumerable<TipoWidget> ConsultarTipo();
}