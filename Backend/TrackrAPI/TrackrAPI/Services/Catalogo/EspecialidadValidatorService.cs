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

        private readonly string MensajeNombreRequerido = "La especialidad es requerida";
        private readonly string MensajeDuplicado = "La especialidad ya existe";

        private static readonly int LongitudNombre = 50;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre} caracteres";
        
        public void ValidarAgregar(EspecialidadFormularioCapturaDto especialidad)
        {
            ValidarRequerido(especialidad);
            ValidarRango(especialidad);
            ValidarDuplicado(especialidad);
        }

       public void ValidarEditar(EspecialidadFormularioCapturaDto especialidad)
        {
            ValidarRequerido(especialidad);
            ValidarRango(especialidad);
            ValidarDuplicado(especialidad);
        }


       public void ValidarRequerido(EspecialidadFormularioCapturaDto especialidad)
        {
            Validator.ValidarRequerido(especialidad.Nombre, MensajeNombreRequerido);
        }

        public void ValidarRango(EspecialidadFormularioCapturaDto especialidad)
        {
            Validator.ValidarLongitudRangoString(especialidad.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarDuplicado(EspecialidadFormularioCapturaDto especialidad)
        {
            var especialidadDuplicado = especialidadRepository.ConsultarPorNombre(especialidad.Nombre);

            if (especialidadDuplicado != null && especialidad.Nombre != especialidadDuplicado.Nombre)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }
    }
}
