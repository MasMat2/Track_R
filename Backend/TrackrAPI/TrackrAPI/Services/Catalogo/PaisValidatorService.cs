using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class PaisValidatorService
    {
        private IPaisRepository paisRepository;
        private IEstadoRepository estadoRepository;

        public PaisValidatorService(IPaisRepository paisRepository,
            IEstadoRepository estadoRepository)
        {
            this.paisRepository = paisRepository;
            this.estadoRepository = estadoRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeExistencia = "El país no existe";
        private readonly string MensajeDuplicado = "El país ya existe";

        private static readonly int LongitudNombre = 50;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";

        private readonly string MensajeDependencia = "El país esta asociado al menos a un estado y no se puede eliminar";

        public void ValidarAgregar(Pais pais)
        {
            ValidarRequerido(pais);
            ValidarRango(pais);
            ValidarDuplicado(pais);
        }

        public void ValidarEditar(Pais pais)
        {
            ValidarRequerido(pais);
            ValidarRango(pais);
            ValidarExistencia(pais.IdPais);
            ValidarDuplicado(pais);
        }

        public void ValidarEliminar(int idPais)
        {
            ValidarExistencia(idPais);
            ValidarDependencia(idPais);
        }

        public void ValidarRequerido(Pais pais)
        {
            Validator.ValidarRequerido(pais.Nombre, MensajeNombreRequerido);
        }

        public void ValidarRango(Pais pais)
        {
            Validator.ValidarLongitudRangoString(pais.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(int idPais)
        {
            Pais pais = paisRepository.Consultar(idPais);

            if (pais == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Pais pais)
        {
            Pais paisDuplicado = paisRepository.Consultar(pais.Nombre);
            if (paisDuplicado != null && pais.IdPais != paisDuplicado.IdPais)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(int idPais)
        {
            var estados = estadoRepository.ConsultarPorPais(idPais);

            if (estados.Any())
            {
                throw new CdisException(MensajeDependencia);
            }
        }

    }
}
