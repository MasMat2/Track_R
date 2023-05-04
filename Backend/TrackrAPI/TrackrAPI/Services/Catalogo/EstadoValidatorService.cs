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

        public void ValidarAgregar(Estado estado)
        {
            ValidarRequerido(estado);
            ValidarRango(estado);
            ValidarDuplicado(estado);
        }

        public void ValidarEditar(Estado estado)
        {
            ValidarRequerido(estado);
            ValidarRango(estado);
            ValidarExistencia(estado.IdEstado);
            ValidarDuplicado(estado);
        }

        public void ValidarEliminar(int idEstado)
        {
            var estado = estadoRepository.Consultar(idEstado);

            ValidarExistencia(idEstado);
            ValidarDependencia(estado);
        }

        public void ValidarRequerido(Estado estado)
        {
            Validator.ValidarRequerido(estado.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(estado.IdPais, MensajePaisRequerido);
        }

        public void ValidarRango(Estado estado)
        {
            Validator.ValidarLongitudRangoString(estado.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(EstadoDto estado)
        {
            if (estado == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idEstado)
        {
            var estado = estadoRepository.Consultar(idEstado);

            if (estado == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Estado estado)
        {
            var estadoDuplicado = estadoRepository.Consultar(estado.Nombre, estado.IdPais);

            if (estadoDuplicado != null && estado.IdEstado != estadoDuplicado.IdEstado)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(Estado estado)
        {
            var estadoDep = estadoRepository.ConsultarDependencias(estado.IdEstado);

            if (estadoDep.Municipio.Any()) {
                throw new CdisException(MensajeDependencia);
            }
        }
    }
}
