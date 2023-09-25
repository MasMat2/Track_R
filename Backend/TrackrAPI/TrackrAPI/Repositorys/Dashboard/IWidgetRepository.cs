using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard;

public interface IWidgetRepository : IRepository<Widget>
{
    public IEnumerable<UsuarioPadecimientosDTO> ConsultarPorUsuario(int idUsuario);
    public Widget? ConsultarPorClave(string clave);
    public IEnumerable<TipoWidget> ConsultarTipo();
}