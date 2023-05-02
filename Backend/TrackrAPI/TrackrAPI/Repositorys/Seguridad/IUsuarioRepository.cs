using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Usuario ConsultarPorCorreo(string correo);
    }
}
