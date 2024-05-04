using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Seguridad;
using System.Linq;

namespace TrackrAPI.Services.Seguridad
{
    public class RolValidatorService
    {
        private IRolRepository rolRepository;
        private IUsuarioRolRepository usuarioRolRepository;
        public RolValidatorService(IRolRepository rolRepository
            , IUsuarioRolRepository usuarioRolRepository)
        {
            this.rolRepository = rolRepository;
            this.usuarioRolRepository = usuarioRolRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerida = "La clave es requerida";
        private readonly string MensajeFiltradoRequerida = "El filtrado es requerido";
        private readonly string MensajeExistencia = "El rol no existe";

        private readonly string MensajeDuplicadoNombre = "El nombre del rol ya existe";
        private readonly string MensajeDuplicadoClave = "La clave del rol ya existe";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudClave = 20;

        private readonly string MensajeUsuarioRolDependencia = "El rol esta asociado al menos a un rol de usuario y no se puede eliminar";
        private readonly string MensajeTipoComisionDetalleDependencia = "El rol esta asociado al menos a un detalle de tipo de comisión y no se puede eliminar";

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

        public void ValidarAgregar(Rol rol)
        {
            ValidarRequerido(rol);
            ValidarRango(rol);
            ValidarDuplicado(rol);
        }

        public void ValidarEditar(Rol rol)
        {
            ValidarRequerido(rol);
            ValidarRango(rol);
            ValidarExistencia(rol.IdRol);
            ValidarDuplicado(rol);
        }

        public void ValidarEliminar(int idRol)
        {
            var rol= rolRepository.Consultar(idRol);

            ValidarExistencia(idRol);
            ValidarDependencia(rol);
        }

        public void ValidarRequerido(Rol rol)
        {
            Validator.ValidarRequerido(rol.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(rol.Clave, MensajeClaveRequerida);
            Validator.ValidarRequerido(rol.Filtrado, MensajeFiltradoRequerida);
        }
        public void ValidarRango(Rol rol)
        {
            Validator.ValidarLongitudRangoString(rol.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(rol.Clave, LongitudClave, MensajeClaveLongitud);
        }
        public void ValidarExistencia(RolDto rol)
        {
            if (rol == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idRol)
        {
            var rol = rolRepository.ConsultarDto(idRol);

            if (rol== null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Rol rol)
        {
            Rol parentescoDuplicadoClave = rolRepository.ConsultarPorClave(rol.Clave);
            Rol parentescoDuplicadoNombre = rolRepository.ConsultarPorNombre(rol.Nombre);

            if (parentescoDuplicadoClave != null && rol.IdRol != parentescoDuplicadoClave.IdRol)
            {
                throw new CdisException(MensajeDuplicadoClave);
            }

            if (parentescoDuplicadoNombre != null && rol.IdRol != parentescoDuplicadoNombre.IdRol)
            {
                throw new CdisException(MensajeDuplicadoNombre);
            }
        }

        public void ValidarDependencia(Rol Rol)
        {
            var usuarioRolDep = rolRepository.ConsultarDependencias(Rol.IdRol);

            if (usuarioRolDep.UsuarioRol.Any())
            {
                throw new CdisException(MensajeUsuarioRolDependencia);
            }

            //if (usuarioRolDep.TipoComisionDetalle.Any())
            //{
            //    throw new CdisException(MensajeTipoComisionDetalleDependencia);
            //}
        }
    }
}
