using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class AreaValidatorService
    {
        private IAreaRepository areaRepository;

        public AreaValidatorService(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerido = "La clave es requerida";

        private readonly string MensajeExistencia = "El área no existe";
        private readonly string MensajeDuplicado = "El área ya existe";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudClave = 20;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre del área son {LongitudNombre } caracteres";
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son { LongitudClave } caracteres";

        private readonly string MensajeNombreFormatoAlfanumerico = "El nombre del área es de formato alfanumérico";

        private readonly string MensajeUsuarioDependencia = "El área esta asociado al menos a una presentación y no se puede eliminar";

        public void ValidarAgregar(Area area)
        {
            ValidarRequerido(area);
            ValidarFormato(area);
            ValidarRango(area);
            ValidarDuplicado(area);
        }

        public void ValidarEditar(Area area)
        {
            ValidarRequerido(area);
            ValidarFormato(area);
            ValidarRango(area);
            ValidarExistencia(area.IdArea);
            ValidarDuplicado(area);
        }

        public void ValidarEliminar(Area area)
        {
            ValidarDependencias(area.IdArea);
        }

        public void ValidarRequerido(Area area)
        {
            Validator.ValidarRequerido(area.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(area.Clave, MensajeClaveRequerido);
        }

        public void ValidarFormato(Area area)
        {
            Validator.ValidarAlfanumerico(area.Nombre, MensajeNombreFormatoAlfanumerico);
        }

        public void ValidarRango(Area area)
        {
            Validator.ValidarLongitudRangoString(area.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(area.Clave, LongitudClave, MensajeClaveLongitud);
        }

        public void ValidarExistencia(int idArea)
        {
            Area area = areaRepository.Consultar(idArea);

            if (area == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Area area)
        {
            var areaDuplicado = areaRepository.Consultar(area.IdArea);

            if (areaDuplicado != null && area.IdArea != areaDuplicado.IdArea)
            {
                throw new CdisException(MensajeDuplicado);
            }

            var areaDuplicado2 = areaRepository.ConsultarExistencia(area.Clave, area.Nombre);

            if (areaDuplicado2 != null && area.IdArea != areaDuplicado2.IdArea)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencias(int idArea)
        {
            var area = areaRepository.ConsultarDependencias(idArea);

            if (area.Presentacion.Any())
            {
                throw new CdisException(MensajeUsuarioDependencia);
            }
        }
    }
}

