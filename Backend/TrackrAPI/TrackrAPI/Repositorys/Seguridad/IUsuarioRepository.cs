using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Usuario Login(string usuario, string contrasena, string clave);
    }
}
