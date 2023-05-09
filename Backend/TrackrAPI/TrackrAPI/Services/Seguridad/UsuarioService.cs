using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Repositorys;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioService
    {
        public IUsuarioRepository usuarioRepository;
        public UsuarioValidatorService usuarioValidatorService;
        public UsuarioService(IUsuarioRepository usuarioRepository,
            UsuarioValidatorService usuarioValidatorService)
        {
            this.usuarioRepository = usuarioRepository;
            this.usuarioValidatorService = usuarioValidatorService;
        }

        public int Agregar(Usuario usuario)
        {
            usuarioValidatorService.ValidarDuplicados(usuario);
            int idUsuario = usuarioRepository.Agregar(usuario).IdUsuario;
            return idUsuario;
        }
    }
}
