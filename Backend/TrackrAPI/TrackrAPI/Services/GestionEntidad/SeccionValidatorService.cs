using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Linq;

namespace TrackrAPI.Services.GestionEntidad
{
    public class SeccionValidatorService
    {
        private readonly ISeccionRepository seccionRepository;

        public SeccionValidatorService(ISeccionRepository seccionRepository)
        {
            this.seccionRepository = seccionRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerida = "La clave es requerida";

        private readonly string MensajeExistencia = "La sección no existe";
        private readonly string MensajeDuplicadoNombre = "La sección con ese nombre ya existe";
        private readonly string MensajeDuplicadoClave = "La sección con esa clave ya existe";
        private readonly string MensajeDependencia = "La sección está relacionada a una entidad y no puede ser eliminada";

        private static readonly int LongitudNombre = 100;
        private static readonly int LongitudClave = 10;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

        public void ValidarAgregar(Seccion seccion)
        {
            ValidarRequerido(seccion);
            ValidarRango(seccion);
            ValidarDuplicado(seccion);
        }

        public void ValidarEditar(Seccion seccion)
        {
            ValidarRequerido(seccion);
            ValidarRango(seccion);
            ValidarExistencia(seccion.IdSeccion);
            ValidarDuplicado(seccion);
        }

        public void ValidarEliminar(int idSeccion)
        {
            var seccion = seccionRepository.ConsultarConDependencias(idSeccion);

            ValidarExistencia(seccion);
            ValidarDependencia(seccion);
        }

        public void ValidarRequerido(Seccion seccion)
        {
            Validator.ValidarRequerido(seccion.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(seccion.Nombre, MensajeClaveRequerida);
        }
        public void ValidarRango(Seccion seccion)
        {
            Validator.ValidarLongitudRangoString(seccion.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(seccion.Clave, LongitudClave, MensajeClaveLongitud);
        }

        public void ValidarExistencia(Seccion seccion)
        {
            if (seccion == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idSeccion)
        {
            var seccion = seccionRepository.Consultar(idSeccion);

            if (seccion == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Seccion seccion)
        {
            var seccionDuplicadoNombre = seccionRepository
                .ConsultarPorNombre(seccion.Nombre);

            var seccionDuplicadoClave = seccionRepository
                .ConsultarPorClave(seccion.Clave);

            if (seccionDuplicadoNombre != null && seccion.IdSeccion != seccionDuplicadoNombre.IdSeccion)
            {
                throw new CdisException(MensajeDuplicadoNombre);
            }

            if (seccionDuplicadoClave != null && seccion.IdSeccion != seccionDuplicadoClave.IdSeccion)
            {
                throw new CdisException(MensajeDuplicadoClave);
            }
        }

        public void ValidarDependencia(Seccion seccion)
        {
            if (seccion.EntidadEstructura.Any())
            {
                throw new CdisException(MensajeDependencia);
            }
        }
    }
}
