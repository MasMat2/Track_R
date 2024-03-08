using TrackrAPI.Models;
using System.Transactions;
using TrackrAPI.Repositorys.PedidoEnLinea;

namespace TrackrAPI.Services.PedidoEnLinea
{
    public class FlujoDetalleResponsableService
    {
        private IFlujoDetalleResponsableRepository flujoDetalleResponsableRepository;
        private FlujoDetalleResponsableValidatorService flujoDetalleResponsableValidatorService;

        public FlujoDetalleResponsableService(
            IFlujoDetalleResponsableRepository flujoDetalleResponsableRepository,
            FlujoDetalleResponsableValidatorService flujoDetalleResponsableValidatorService)
        {
            this.flujoDetalleResponsableRepository = flujoDetalleResponsableRepository;
            this.flujoDetalleResponsableValidatorService = flujoDetalleResponsableValidatorService;
        }

        public List<FlujoDetalleResponsable> ConsultarPorFlujoDetalle(int idFlujoDetalle)
        {
            return flujoDetalleResponsableRepository.ConsultarPorFlujoDetalle(idFlujoDetalle);
        }

        public void Agregar(FlujoDetalleResponsable flujoDetalleResponsable)
        {
            flujoDetalleResponsableValidatorService.ValidarAgregar(flujoDetalleResponsable);
            flujoDetalleResponsableRepository.Agregar(flujoDetalleResponsable);
        }

        public void Editar(FlujoDetalleResponsable flujoDetalleResponsable)
        {
            flujoDetalleResponsableValidatorService.ValidarEditar(flujoDetalleResponsable);
            flujoDetalleResponsableRepository.Editar(flujoDetalleResponsable);
        }

        public void Eliminar(int idFlujoDetalleResponsable)
        {
            FlujoDetalleResponsable flujoDetalleResponsable = flujoDetalleResponsableRepository.Consultar(idFlujoDetalleResponsable);

            flujoDetalleResponsableValidatorService.ValidarEliminar(idFlujoDetalleResponsable);
            flujoDetalleResponsableRepository.Eliminar(flujoDetalleResponsable);
        }

        public void Guardar(List<FlujoDetalleResponsable> responsableList, int idFlujoDetalle)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                List<FlujoDetalleResponsable> responsableActual = flujoDetalleResponsableRepository.ConsultarPorFlujoDetalle(idFlujoDetalle);
                var responsableEliminado = responsableActual.Except(responsableList);
                var responsableAgregado = responsableList.Except(responsableActual);

                foreach (FlujoDetalleResponsable responsable in responsableEliminado)
                {
                    Eliminar(responsable.IdFlujoDetalleResponsable);
                }

                foreach (FlujoDetalleResponsable responsable in responsableAgregado)
                {
                    Agregar(responsable);
                }

                scope.Complete();
            }
        }
    }
}
