using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioValidatorService
    {
        private IUsuarioRepository usuarioRepository;

        public UsuarioValidatorService(
            IUsuarioRepository usuarioRepository
        )
        {
            this.usuarioRepository = usuarioRepository;
        }

        public void ValidarDuplicados(Usuario usuario)
        {
            Usuario usuarioExistente = this.usuarioRepository.ConsultarPorCorreo(usuario.Correo);

            if (usuarioExistente != null && usuario.IdUsuario != usuarioExistente.IdUsuario && usuario.Correo != null)
            {
                throw new CdisException($@"El usuario '{usuario.Correo}' ya se encuentra registrado");
            }
        }
    }
}
