using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioRolValidatorService
    {
        private IUsuarioRolRepository usuarioRolRepository;
        private readonly string MensajeRequeridoRol = "El rol es requerido";
        public UsuarioRolValidatorService(IUsuarioRolRepository usuarioRolRepository)
        {
            this.usuarioRolRepository = usuarioRolRepository;
        }

        public void ValidarAgregar(UsuarioRol usuarioRol)
        {
            ValidarRequerido(usuarioRol);
        }

        public void ValidarEditar(UsuarioRol usuarioRol)
        {
            ValidarRequerido(usuarioRol);
            ValidarDuplicado(usuarioRol);
        }

        public void ValidarRequerido(UsuarioRol usuarioRol)
        {
            Validator.ValidarRequerido(usuarioRol.IdRol, MensajeRequeridoRol);
            ValidarDuplicado(usuarioRol);
        }

        public void ValidarDuplicado(UsuarioRol usuarioRol)
        {
            UsuarioRol usuarioRolExistente;

            usuarioRolExistente = usuarioRolRepository.Consultar(usuarioRol.IdUsuario, usuarioRol.IdRol);

            if (usuarioRolExistente != null && usuarioRolExistente.IdUsuarioRol != usuarioRol.IdUsuarioRol)
            {
                throw new CdisException("El usuario ya tiene asignado el rol");
            }
        }
    }
}
