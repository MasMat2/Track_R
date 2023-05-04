using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioLocacionValidatorService
    {
        private IUsuarioLocacionRepository usuarioLocacionRepository;

        private readonly string MensajeLocacionRequerida = "La locación es requerida";
        private readonly string MensajePerfilRequerido = "El perfil es requerido";
        private readonly string MensajeUsuarioRequerido = "El usuario es requerido";

        public UsuarioLocacionValidatorService(IUsuarioLocacionRepository usuarioLocacionRepository)
        {
            this.usuarioLocacionRepository = usuarioLocacionRepository;
        }

        public void ValidarAgregar(UsuarioLocacion usuarioLocacion)
        {
            ValidarRequerido(usuarioLocacion);
            ValidarDuplicado(usuarioLocacion);
        }

        public void ValidarEditar(UsuarioLocacion usuarioLocacion)
        {
            ValidarRequerido(usuarioLocacion);
            ValidarDuplicado(usuarioLocacion);
        }

        public void ValidarRequerido(UsuarioLocacion usuarioLocacion)
        {
            Validator.ValidarRequerido(usuarioLocacion.IdLocacion, MensajeLocacionRequerida);
            Validator.ValidarRequerido(usuarioLocacion.IdPerfil, MensajePerfilRequerido);
            Validator.ValidarRequerido(usuarioLocacion.IdUsuario, MensajeUsuarioRequerido);
        }

        public void ValidarDuplicado(UsuarioLocacion usuarioLocacion)
        {
            UsuarioLocacion permisoConsultado = usuarioLocacionRepository.Consultar(usuarioLocacion.IdUsuario, usuarioLocacion.IdLocacion);

            if (permisoConsultado != null && permisoConsultado.IdUsuarioLocacion != usuarioLocacion.IdUsuarioLocacion)
            {
                throw new CdisException("El usuario ya tiene un permiso en esta locación");
            }
        }

    }
}
