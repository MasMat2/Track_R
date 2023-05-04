using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Dtos.PedidoEnLinea;
using TrackrAPI.Repositorys.PedidoEnLinea;

namespace TrackrAPI.Services
{
    public class FlujoDetalleValidatorService
    {
        private IFlujoDetalleRepository flujoDetalleRepository;
        private IFlujoRepository flujoRepository;
        private IAreaRepository areaRepository;
        private IEstatusPedidoRepository estatusPedidoRepository;

        private static readonly string MensajeExistencia = "El detalle de flujo no existe";
        private static readonly string MensajeOrdenDuplicado = "Ya existe un detalle de flujo con ese orden";

        private static readonly string MensajeIdFlujoRequerido = "El flujo es requerido";
        private static readonly string MensajeOrdenRequerido = "El orden es requerido";
        private static readonly string MensajeRolRequerido = "El rol es requerido";
        private static readonly string MensajeEstatusPedidoRequerido = "El estatus de pedido es requerido";

        private static readonly string MensajeFlujoExistencia = "El flujo no existe";
        private static readonly string MensajeAreaExistencia = "El área no existe";
        private static readonly string MensajeEstatusPedidoExistencia = "El estatus de pedido no existe";

        private static readonly int LongitudMaximaOrden = 10;

        private static readonly string MensajeLongitudOrden = $"La longitud máxima del Orden son {LongitudMaximaOrden}";

        public FlujoDetalleValidatorService(
            IFlujoDetalleRepository flujoDetalleRepository,
            IFlujoRepository flujoRepository,
            IAreaRepository areaRepository,
            IEstatusPedidoRepository estatusPedidoRepository
        )
        {
            this.flujoDetalleRepository = flujoDetalleRepository;
            this.flujoRepository = flujoRepository;
            this.areaRepository = areaRepository;
            this.estatusPedidoRepository = estatusPedidoRepository;
        }

        public void ValidarAgregar(FlujoDetalle flujoDetalle)
        {
            ValidarRequerido(flujoDetalle);
            ValidarRango(flujoDetalle);
            ValidarLlavesForaneas(flujoDetalle);
            ValidarOrden(flujoDetalle);
        }

        public void ValidarEditar(FlujoDetalle flujoDetalle)
        {
            ValidarRequerido(flujoDetalle);
            ValidarRango(flujoDetalle);
            ValidarExistencia(flujoDetalle.IdFlujoDetalle);
            ValidarLlavesForaneas(flujoDetalle);
            ValidarOrden(flujoDetalle);
        }

        public void ValidarEliminar(int idFlujoDetalle)
        {
            ValidarExistencia(idFlujoDetalle);
            ValidarDependencia(idFlujoDetalle);
        }

        private void ValidarRequerido(FlujoDetalle flujoDetalle)
        {
            Validator.ValidarRequerido(flujoDetalle.IdFlujo, MensajeIdFlujoRequerido);
            Validator.ValidarRequerido(flujoDetalle.Orden, MensajeOrdenRequerido);
            Validator.ValidarRequerido(flujoDetalle.IdRol, MensajeRolRequerido);
            Validator.ValidarRequerido(flujoDetalle.IdEstatusPedido, MensajeEstatusPedidoRequerido);
        }

        private void ValidarRango(FlujoDetalle flujoDetalle)
        {
            Validator.ValidarRangoEntero(flujoDetalle.Orden, 1, LongitudMaximaOrden, MensajeLongitudOrden);
        }

        private void ValidarLlavesForaneas(FlujoDetalle flujoDetalle)
        {
            Flujo flujo = flujoRepository.Consultar(flujoDetalle.IdFlujo);
            EstatusPedido estatusPedido = estatusPedidoRepository.Consultar(flujoDetalle.IdEstatusPedido);

            if (flujo == null)
                throw new CdisException(MensajeFlujoExistencia);

            if (estatusPedido == null)
                throw new CdisException(MensajeEstatusPedidoExistencia);

            if (flujoDetalle.IdArea > 0)
            {
                Area area = areaRepository.Consultar((int)flujoDetalle.IdArea);
                if (area == null)
                    throw new CdisException(MensajeAreaExistencia);
            }
        }

        private void ValidarExistencia(int idFlujoDetalle)
        {
            FlujoDetalle flujoDetalle = flujoDetalleRepository.Consultar(idFlujoDetalle);

            if (flujoDetalle == null)
                throw new CdisException(MensajeExistencia);
        }

        private void ValidarOrden(FlujoDetalle flujoDetalle)
        {
            List<FlujoDetalleDto> detalles = flujoDetalleRepository.ConsultarPorFlujo(flujoDetalle.IdFlujo).ToList();

            if (detalles.Any(d => flujoDetalle.IdFlujoDetalle != d.IdFlujoDetalle && flujoDetalle.Orden == d.Orden))
                throw new CdisException(MensajeOrdenDuplicado);
        }

        private void ValidarDependencia(int idFlujoDetalle)
        {
            bool tieneDependencias = flujoDetalleRepository.TieneDependencias(idFlujoDetalle);

            if (tieneDependencias)
            {
                FlujoDetalle flujoDetalle = flujoDetalleRepository.Consultar(idFlujoDetalle);

                throw new CdisException($"El detalle de flujo No. {flujoDetalle.Orden} está siendo utilizado por al menos un pedido y no se puede eliminar");
            }
        }

        public void ValidarResponsables(FlujoDetalleDto flujoDetalleDto)
        {
            if (flujoDetalleDto.SolicitarResponsable == true && !(flujoDetalleDto.IdsUsuariosResponsables.Length > 0))
            {
                throw new CdisException($"Es necesario asignar al menos un responsable para el detalle de flujo No. {flujoDetalleDto.Orden}");
            }
        }
    }
}
