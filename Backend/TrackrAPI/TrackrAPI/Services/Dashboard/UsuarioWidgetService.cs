using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Dashboard;

namespace TrackrAPI.Services.Dashboard
{
    public class UsuarioWidgetService
    {
        private readonly IUsuarioWidgetRepository _usuarioWidgetRepository;
        private readonly IWidgetRepository _widgetRepository;

        public UsuarioWidgetService(IUsuarioWidgetRepository usuarioWidgetRepository, IWidgetRepository widgetRepository)
        {
            _usuarioWidgetRepository = usuarioWidgetRepository;
            _widgetRepository = widgetRepository;
        }

        public IEnumerable<string> ConsultarPorUsuario(int usuarioId)
        {
            return _usuarioWidgetRepository.ConsultarPorUsuario(usuarioId)
                .Select(uw => uw.IdWidgetNavigation.Clave);
        }

        public void modificarSeleccionWidgets(int usuarioId, IEnumerable<string> widgets)
        {
            _usuarioWidgetRepository.EliminarPorUsuario(usuarioId);
          
            if(widgets.Any())
            {
            foreach ( var widget in widgets) {

                var widgetExistente = _widgetRepository.consultarPorClave(widget);

                if (widgetExistente == null)
                    throw new CdisException("Clave de widget no reconocida");

                var nuevoUsuarioWidget = new UsuarioWidget
                {
                    IdUsuario = usuarioId,
                    IdWidget = widgetExistente.IdWidget,
                };

                _usuarioWidgetRepository.Agregar(nuevoUsuarioWidget);
            }
            }
                
           
        }

        public void AgregarWidgetPadecimiento(int idUsuario , int idPadecimiento){
            var widget = _widgetRepository.ConsultarPorPadecimiento(idPadecimiento);

            if(widget == null){
                throw new CdisException("No se encontró widget para el padecimiento");
            }

            var nuevoUsuarioWidget = new UsuarioWidget
            {
                IdUsuario = idUsuario,
                IdWidget = widget.IdWidget,
            };

            _usuarioWidgetRepository.Agregar(nuevoUsuarioWidget);
        }
    }
}