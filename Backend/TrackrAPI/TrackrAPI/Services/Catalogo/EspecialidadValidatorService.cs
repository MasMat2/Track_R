using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class EspecialidadValidatorService
    {
        private IEspecialidadRepository especialidadRepository;

        public EspecialidadValidatorService(IEspecialidadRepository especialidadRepository)
        {
            this.especialidadRepository = especialidadRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeIDRequerido = "El ID es requerido";
        private readonly string MensajeExistencia = "El ID no existe";
        private readonly string MensajeDuplicado = "El ID ya existe";


        private static readonly int LongitudNombre = 50;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";

        public void ValidarAgregar(EspecialidadFormularioCapturaDto especialidad)
        {
            ValidarRequerido(especialidad);
            ValidarRango(especialidad);
            ValidarDuplicado(especialidad);
        }

        public void ValidarEditar(EspecialidadFormularioCapturaDto especialidad)
        {
            ValidarExistencia(especialidad.IdEspecialidad);
            ValidarRequerido(especialidad);
            ValidarRango(especialidad);
            ValidarDuplicado(especialidad);
        }

        public void ValidarEliminar(int idEspecialidad)
        {
            ValidarExistencia(idEspecialidad);
            ValidarDependencia(idEspecialidad);
        }

        public void ValidarRequerido(EspecialidadFormularioCapturaDto especialidad)
        {
            Validator.ValidarRequerido(especialidad.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(especialidad.IdEspecialidad, MensajePaisRequerido);
        }

        public void ValidarRango(EspecialidadFormularioCapturaDto especialidad)
        {
            Validator.ValidarLongitudRangoString(especialidad.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(int idEspecialidad)
        {
            var especialdiad = especialidadRepository.ConsultarDependencias(idEspecialidad);

            if (idEspecialidad is null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(EspecialidadFormularioCapturaDto especialidad)
        {
            var especialidadDuplicado = especialidadRepository.Consultar(especialidad.Nombre, especialidad.IdEspecialidad);

            if (especialidadDuplicado != null && especialidad.IdEspecialidad != especialidadDuplicado.IdEspecialidad)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }
    }
}
