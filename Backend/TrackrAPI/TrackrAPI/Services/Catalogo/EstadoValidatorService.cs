using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class EstadoValidatorService
    {
        private IEstadoRepository estadoRepository;

        public EstadoValidatorService(IEstadoRepository estadoRepository)
        {
            this.estadoRepository = estadoRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajePaisRequerido = "El país es requerido";
        private readonly string MensajeExistencia = "El estado no existe";
        private readonly string MensajeDuplicado = "El estado ya existe";

        private readonly string MensajeDependencia = "El estado esta asociado al menos a un municipio y no se puede eliminar";

        private static readonly int LongitudNombre = 50;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";

        public void ValidarAgregar(EstadoFormularioCapturaDto estado)
        {
            ValidarRequerido(estado);
            ValidarRango(estado);
            ValidarDuplicado(estado);
        }

        public void ValidarEditar(EstadoFormularioCapturaDto estado)
        {
            ValidarExistencia(estado.IdEstado);
            ValidarRequerido(estado);
            ValidarRango(estado);
            ValidarDuplicado(estado);
        }

        public void ValidarEliminar(int idEstado)
        {
            ValidarExistencia(idEstado);
            ValidarDependencia(idEstado);
        }

        public void ValidarRequerido(EstadoFormularioCapturaDto estado)
        {
            Validator.ValidarRequerido(estado.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(estado.IdPais, MensajePaisRequerido);
        }

        public void ValidarRango(EstadoFormularioCapturaDto estado)
        {
            Validator.ValidarLongitudRangoString(estado.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(int idEstado)
        {
            var estado = estadoRepository.ConsultarDependencias(idEstado);

            if (estado is null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(EstadoFormularioCapturaDto estado)
        {
            var estadoDuplicado = estadoRepository.Consultar(estado.Nombre, estado.IdPais);

            if (estadoDuplicado != null && estado.IdEstado != estadoDuplicado.IdEstado)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(int idEstado)
        {
            var estadoDep = estadoRepository.ConsultarDependencias(idEstado)!;

            if (estadoDep.Municipio.Any()) {
                throw new CdisException(MensajeDependencia);
            }
        }
    }
}
