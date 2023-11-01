using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard
{
    public interface IUsuarioWidgetRepository : IRepository<UsuarioWidget>
    {
        public IEnumerable<UsuarioWidget> ConsultarPorUsuario(int usuarioId);
    }
}