using TrackrAPI.Dtos.PedidoEnLinea;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Transactions;
using TrackrAPI.Repositorys.PedidoEnLinea;
using TrackrAPI.Services.PedidoEnLinea;

namespace TrackrAPI.Services
{
    public class FlujoDetalleService
    {
        IFlujoDetalleRepository flujoDetalleRepository;
        FlujoDetalleValidatorService flujoDetalleValidatorService;
        FlujoDetalleResponsableService flujoDetalleResponsableService;

        public FlujoDetalleService(
            IFlujoDetalleRepository flujoDetalleRepository,
            FlujoDetalleValidatorService flujoDetalleValidatorService,
            FlujoDetalleResponsableService flujoDetalleResponsableService
        )
        {
            this.flujoDetalleRepository = flujoDetalleRepository;
            this.flujoDetalleValidatorService = flujoDetalleValidatorService;
            this.flujoDetalleResponsableService = flujoDetalleResponsableService;
        }

        public IEnumerable<FlujoDetalleDto> ConsultarPorFlujo(int idFlujo)
        {
            return flujoDetalleRepository.ConsultarPorFlujo(idFlujo);
        }

        public IEnumerable<FlujoDetalleGridDto> ConsultarParaGrid(int idFlujo)
        {
            return flujoDetalleRepository.ConsultarParaGrid(idFlujo);
        }

        public FlujoDetalleDto ConsultarPrimerFlujoPorPresentacion(int idPresentacion, int idCompania, string claveOpcionVenta)
        {
            FlujoDetalle flujoDetalle = flujoDetalleRepository.ConsultarPrimerFlujoPorPresentacion(idPresentacion,idCompania,claveOpcionVenta);

            return new FlujoDetalleDto()
            {
                IdFlujo = flujoDetalle.IdFlujo,
                IdFlujoDetalle = flujoDetalle.IdFlujoDetalle,
                Orden = flujoDetalle.Orden,
                NombreFlujo = flujoDetalle.IdFlujoNavigation.Nombre
            };
        }

        public FlujoDetalleDto ConsultarDto(int idFlujoDetalle)
        {
            FlujoDetalle flujoDetalle = flujoDetalleRepository.Consultar(idFlujoDetalle);

            return new FlujoDetalleDto()
            {
                IdFlujo = flujoDetalle.IdFlujo,
                IdFlujoDetalle = flujoDetalle.IdFlujoDetalle,
                IdRol = flujoDetalle.IdRol,
                IdEstatusPedido = flujoDetalle.IdEstatusPedido,
                Orden = flujoDetalle.Orden,
                SolicitarResponsable = flujoDetalle.SolicitarResponsable,
                Responsables = flujoDetalle.ObtenerUsuariosResponsables()
            };
        }

        public FlujoDetalle ConsultarFlujoDetalleConsecutivo(int idFlujoDetalle)
        {
            FlujoDetalle flujoDetalle = flujoDetalleRepository.Consultar(idFlujoDetalle);

            var detalles =  flujoDetalleRepository.ConsultarPorFlujo(flujoDetalle.IdFlujo);
            var flujo = detalles.Where(fd => fd.Orden == flujoDetalle.Orden + 1).FirstOrDefault();

            if (flujo != null)
            {
                FlujoDetalle flujoDetalleSiguiente = new()
                {
                    IdFlujoDetalle = flujo.IdFlujoDetalle,
                    IdFlujo = flujo.IdFlujo,
                    Orden = flujo.Orden,
                    IdEstatusPedido = flujo.IdEstatusPedido,
                    IdRol = flujo.IdRol,
                    SolicitarResponsable = flujo.SolicitarResponsable
                };

                return flujoDetalleSiguiente;
            }

            return flujoDetalle;
        }

        public void Agregar(FlujoDetalleDto flujoDetalleDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                FlujoDetalle flujoDetalle = new()
                {
                    IdFlujoDetalle = flujoDetalleDto.IdFlujoDetalle,
                    IdFlujo = flujoDetalleDto.IdFlujo,
                    Orden = flujoDetalleDto.Orden,
                    IdArea = flujoDetalleDto.IdArea,
                    IdEstatusPedido = flujoDetalleDto.IdEstatusPedido,
                    IdRol = flujoDetalleDto.IdRol,
                    SolicitarResponsable = flujoDetalleDto.SolicitarResponsable
                };

                flujoDetalleValidatorService.ValidarResponsables(flujoDetalleDto);
                flujoDetalleValidatorService.ValidarAgregar(flujoDetalle);
                flujoDetalleRepository.Agregar(flujoDetalle);

                var responsables = flujoDetalleDto.IdsUsuariosResponsables.Select(idUsuario => new FlujoDetalleResponsable
                {
                    IdFlujoDetalle = flujoDetalle.IdFlujoDetalle,
                    IdUsuario = idUsuario
                })
                .ToList();

                flujoDetalleResponsableService.Guardar(responsables, flujoDetalle.IdFlujoDetalle);

                scope.Complete();
            }
        }

        public void Editar(FlujoDetalleDto flujoDetalleDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                FlujoDetalle flujoDetalle = new()
                {
                    IdFlujoDetalle = flujoDetalleDto.IdFlujoDetalle,
                    IdFlujo = flujoDetalleDto.IdFlujo,
                    Orden = flujoDetalleDto.Orden,
                    IdArea = flujoDetalleDto.IdArea,
                    IdEstatusPedido = flujoDetalleDto.IdEstatusPedido,
                    IdRol = flujoDetalleDto.IdRol,
                    SolicitarResponsable = flujoDetalleDto.SolicitarResponsable
                };

                flujoDetalleValidatorService.ValidarResponsables(flujoDetalleDto);
                flujoDetalleValidatorService.ValidarEditar(flujoDetalle);
                flujoDetalleRepository.Editar(flujoDetalle);

                var responsables = flujoDetalleDto.IdsUsuariosResponsables.Select(idUsuario => new FlujoDetalleResponsable
                {
                    IdFlujoDetalle = flujoDetalle.IdFlujoDetalle,
                    IdUsuario = idUsuario
                })
                .ToList();

                flujoDetalleResponsableService.Guardar(responsables, flujoDetalle.IdFlujoDetalle);

                scope.Complete();
            }
        }

        public void Eliminar(int idFlujoDetalle)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                flujoDetalleValidatorService.ValidarEliminar(idFlujoDetalle);

                // Se eliminan los responsables asignados al flujo detalle
                var flujoDetalleResponsableList = flujoDetalleResponsableService.ConsultarPorFlujoDetalle(idFlujoDetalle);
                foreach (var fdr in flujoDetalleResponsableList)
                {
                    flujoDetalleResponsableService.Eliminar(fdr.IdFlujoDetalleResponsable);
                }

                // Se elimina el flujo detalle
                FlujoDetalle flujoDetalle = flujoDetalleRepository.Consultar(idFlujoDetalle);
                flujoDetalleRepository.Eliminar(flujoDetalle);

                ts.Complete();
            }
        }
    }
}
