using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Repositorys;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioService
    {
        public IUsuarioRepository usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public int Agregar(Usuario usuario)
        {
            int idUsuario = usuarioRepository.Agregar(usuario).IdUsuario;
            return idUsuario;
        }
    }
}
