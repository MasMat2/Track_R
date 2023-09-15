using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard;

public interface IWidgetRepository : IRepository<Widget>
{
    public IEnumerable<Widget> Consultar();
    public Widget? ConsultarPorClave(string clave);
    public IEnumerable<TipoWidget> ConsultarTipo();
}