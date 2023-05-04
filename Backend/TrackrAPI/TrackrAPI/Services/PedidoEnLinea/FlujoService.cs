using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Transactions;
using Trackr.Dtos.PedidoEnLinea;
using TrackrAPI.Repositorys.PedidoEnLinea;
using TrackrAPI.Dtos.PedidoEnLinea;

namespace TrackrAPI.Services.PedidoEnLinea
{
    public class FlujoService
    {
        private IFlujoRepository flujoRepository;
        private IFlujoDetalleRepository flujoDetalleRepository;
        private FlujoValidatorService flujoValidatorService;
        private FlujoDetalleService flujoDetalleService;

        public FlujoService(
            IFlujoRepository flujoRepository,
            IFlujoDetalleRepository flujoDetalleRepository,
            FlujoValidatorService flujoValidatorService,
            FlujoDetalleService flujoDetalleService
        )
        {
            this.flujoRepository = flujoRepository;
            this.flujoDetalleRepository = flujoDetalleRepository;
            this.flujoValidatorService = flujoValidatorService;
            this.flujoDetalleService = flujoDetalleService;
        }

        public IEnumerable<FlujoDto> ConsultarTodosParaSelector(int idCompania)
        {
            return flujoRepository.ConsultarTodosParaSelector(idCompania);
        }

        public IEnumerable<FlujoDto> ConsultarTodosParaGrid(int idCompania, int idUsuario)
        {
            return flujoRepository.ConsultarTodosParaGrid(idCompania, idUsuario);
        }

        public FlujoDto ConsultarPorPresentacionOpcionVenta(int idPresentacion, int idCompania, string claveOpcionVenta)
        {
            Flujo flujo = flujoRepository.ConsultarPorPresentacionOpcionVenta(idPresentacion, idCompania, claveOpcionVenta);

            if (flujo == null)
                return null;

            return new FlujoDto()
            {
                IdFlujo = flujo.IdFlujo,
                Clave = flujo.Clave,
                Nombre = flujo.Nombre,
                EsDefault = flujo.EsDefault,
                IdTipoFlujo = flujo.IdTipoFlujo,
                IdRol = flujo.IdRol
            };
        }

        public FlujoDto ConsultarDto(int idFlujo)
        {
            Flujo flujo = flujoRepository.Consultar(idFlujo);

            if (flujo == null)
                return null;

            return new FlujoDto()
            {
                IdFlujo = flujo.IdFlujo,
                Clave = flujo.Clave,
                Nombre = flujo.Nombre,
                EsDefault = flujo.EsDefault,
                IdTipoFlujo = flujo.IdTipoFlujo,
                IdRol = flujo.IdRol
            };
        }

        public void Agregar(Flujo flujo)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                flujoValidatorService.ValidarAgregar(flujo);
                Flujo flujoEstandarAntiguo = flujoValidatorService.ValidarFlujoEstandar(flujo);

                flujoRepository.Agregar(flujo);

                if (flujoEstandarAntiguo != null)
                {
                    flujoEstandarAntiguo.EsDefault = false;
                    flujoRepository.Editar(flujoEstandarAntiguo);
                }

                ts.Complete();
            }
        }

        public void Editar(Flujo flujo)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                flujoValidatorService.ValidarEditar(flujo);
                Flujo flujoEstandarAntiguo = flujoValidatorService.ValidarFlujoEstandar(flujo);

                flujoRepository.Editar(flujo);

                if (flujoEstandarAntiguo != null)
                {
                    flujoEstandarAntiguo.EsDefault = false;
                    flujoRepository.Editar(flujoEstandarAntiguo);
                }

                ts.Complete();
            }
        }

        public void Eliminar(int idFlujo)
        {
            flujoValidatorService.ValidarEliminar(idFlujo);

            using (TransactionScope ts = new TransactionScope())
            {
                List<FlujoDetalleDto> detalles = flujoDetalleRepository.ConsultarPorFlujo(idFlujo).ToList();

                foreach(FlujoDetalleDto detalle in detalles)
                {
                    try
                    {
                        if (detalle.IdFlujoDetalle > 0)
                        {
                            flujoDetalleService.Eliminar(detalle.IdFlujoDetalle);
                        }
                    }
                    catch (CdisException ex)
                    {
                        throw new CdisException("No se pudo eliminar el flujo: " + ex.ErrorMessage);
                    }
                }

                Flujo flujo = flujoRepository.Consultar(idFlujo);
                flujoRepository.Eliminar(flujo);

                ts.Complete();
            }
        }
    }
}
