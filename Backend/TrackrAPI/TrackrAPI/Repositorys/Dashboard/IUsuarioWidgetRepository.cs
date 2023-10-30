using DocumentFormat.OpenXml.InkML;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard
{
    public interface IUsuarioWidgetRepository : IRepository<UsuarioWidget>
    {
        public IEnumerable<UsuarioWidget> ConsultarPorUsuario(int usuarioId);

        public void EliminarPorUsuario(int usuarioId);

        public UsuarioWidget ConsultarSeleccionadoPorClave(int usuarioId, string clave);

    }


}