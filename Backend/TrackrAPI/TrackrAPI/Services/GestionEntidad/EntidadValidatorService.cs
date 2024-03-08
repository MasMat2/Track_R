using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;

namespace TrackrAPI.Services.GestionEntidad
{
    public class EntidadValidatorService
    {
        private readonly IEntidadRepository entidadRepository;

        public EntidadValidatorService(IEntidadRepository entidadRepository)
        {
            this.entidadRepository = entidadRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerido = "La clave es requerida";

        private readonly string MensajeExistencia = "La configuración de entidad no existe";
        private readonly string MensajeDuplicado = "La configuración de entidad ya existe";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudClave = 3;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

        public void ValidarAgregar(Entidad entidad)
        {
            ValidarRequerido(entidad);
            ValidarRango(entidad);
            ValidarDuplicado(entidad);
        }

        public void ValidarEditar(Entidad entidad)
        {
            ValidarRequerido(entidad);
            ValidarRango(entidad);
            ValidarExistencia(entidad.IdEntidad);
            ValidarDuplicado(entidad);
        }

        public void ValidarEliminar(int identidad)
        {
            var entidad = entidadRepository.Consultar(identidad);

            ValidarExistencia(entidad.IdEntidad);
        }

        private void ValidarRequerido(Entidad entidad)
        {
            Validator.ValidarRequerido(entidad.Clave, MensajeClaveRequerido);
            Validator.ValidarRequerido(entidad.Nombre, MensajeNombreRequerido);
        }

        private void ValidarRango(Entidad entidad)
        {
            Validator.ValidarLongitudRangoString(entidad.Clave, LongitudClave, MensajeClaveLongitud);
            Validator.ValidarLongitudRangoString(entidad.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        private void ValidarExistencia(int idEntidad)
        {
            var estado = entidadRepository.Consultar(idEntidad);

            if (estado == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        private void ValidarDuplicado(Entidad entidad)
        {
            var entidadDuplicado = entidadRepository.Consultar(entidad.Clave, entidad.Nombre);

            if (entidadDuplicado != null && entidadDuplicado.IdEntidad != entidad.IdEntidad)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }
    }
}
