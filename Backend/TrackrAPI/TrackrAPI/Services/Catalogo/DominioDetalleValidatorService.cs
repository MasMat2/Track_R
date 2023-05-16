using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class DominioDetalleValidatorService
    {
        private IDominioDetalleRepository dominioDetalleRepository;

        public DominioDetalleValidatorService(IDominioDetalleRepository dominioDetalleRepository)
        {
            this.dominioDetalleRepository = dominioDetalleRepository;
        }

        private readonly string MensajeDominioRequerido = "El dominio es requerido";
        private readonly string MensajeValorRequerido = "El valor es requerido";
        private readonly string MensajeExistencia = "El dominio detalle no existe";
        private readonly string MensajeDuplicado = "El dominio detalle ya existe";

        private static readonly int LongitudValor = 50;

        private readonly string MensajeValorLongitud = $"La longitud máxima del nombre son { LongitudValor } caracteres";

        public void ValidarAgregar(DominioDetalle dominioDetalle)
        {
            ValidarRequerido(dominioDetalle);
            ValidarRango(dominioDetalle);
            ValidarDuplicado(dominioDetalle);
        }

        public void ValidarEditar(DominioDetalle dominioDetalle)
        {
            ValidarRequerido(dominioDetalle);
            ValidarRango(dominioDetalle);
            ValidarExistencia(dominioDetalle.IdDominioDetalle);
            ValidarDuplicado(dominioDetalle);
        }

        public void ValidarEliminar(int idDominioDetalle)
        {
            var dominioDetalle = dominioDetalleRepository.Consultar(idDominioDetalle);

            ValidarExistencia(idDominioDetalle);
            ValidarDependencia(idDominioDetalle);
        }

        public void ValidarRequerido(DominioDetalle dominioDetalle)
        {
            Validator.ValidarRequerido(dominioDetalle.Valor, MensajeValorRequerido);
            Validator.ValidarRequerido(dominioDetalle.IdDominio, MensajeDominioRequerido);
        }

        public void ValidarRango(DominioDetalle dominioDetalle)
        {
            Validator.ValidarLongitudRangoString(dominioDetalle.Valor, LongitudValor, MensajeValorLongitud);
        }

        public void ValidarExistencia(DominioDetalleDto dominioDetalleDto)
        {
            if (dominioDetalleDto == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idDominioDetalle)
        {
            var dominioDetalle = dominioDetalleRepository.Consultar(idDominioDetalle);

            if (dominioDetalle == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(DominioDetalle dominioDetalle)
        {
            var dominioDetalleDuplicado = dominioDetalleRepository.Consultar(dominioDetalle.Valor, dominioDetalle.IdDominio);

            if (dominioDetalleDuplicado != null && dominioDetalle.IdDominioDetalle != dominioDetalleDuplicado.IdDominioDetalle)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(int idDominioDetalle)
        {
        }
    }
}
