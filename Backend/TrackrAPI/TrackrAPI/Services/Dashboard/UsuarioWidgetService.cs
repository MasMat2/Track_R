using TrackrAPI.Models;
using TrackrAPI.Repositorys.Dashboard;

namespace TrackrAPI.Services.Dashboard
{
    public class UsuarioWidgetService
    {
        private readonly IUsuarioWidgetRepository _usuarioWidgetRepository;

        public UsuarioWidgetService(IUsuarioWidgetRepository usuarioWidgetRepository)
        {
            _usuarioWidgetRepository = usuarioWidgetRepository;
        }

        public IEnumerable<string> ConsultarPorUsuario(int usuarioId)
        {
            return _usuarioWidgetRepository.ConsultarPorUsuario(usuarioId)
                .Select(uw => uw.IdWidgetNavigation.Clave);
        }
    }
}