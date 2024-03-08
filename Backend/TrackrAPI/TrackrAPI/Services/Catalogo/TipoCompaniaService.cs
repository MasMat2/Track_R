using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using System.Transactions;

namespace TrackrAPI.Services.Catalogo
{
    public class TipoCompaniaService
    {
        private ITipoCompaniaRepository tipoCompaniaRepository;
        private TipoCompaniaValidatorService tipoCompaniaValidatorService;

        public TipoCompaniaService(ITipoCompaniaRepository tipoCompaniaRepository, TipoCompaniaValidatorService tipoCompaniaValidatorService)
        {
            this.tipoCompaniaRepository = tipoCompaniaRepository;
            this.tipoCompaniaValidatorService = tipoCompaniaValidatorService;
        }

        public TipoCompania Consultar(int idTipoCompaia)
        {
            return tipoCompaniaRepository.Consultar(idTipoCompaia);
        }

        public IEnumerable<TipoCompaniaSelectorDto> ConsultarParaSelector()
        {
            return tipoCompaniaRepository.ConsultarParaSelector();
        }

        public IEnumerable<TipoCompania> ConsultarTodosParaGrid()
        {
            return tipoCompaniaRepository.ConsultarTodosParaGrid();
        }

        public TipoCompania ConsultarPorClave(string claveTipoCompania)
        {
            return tipoCompaniaRepository.ConsultarPorClave(claveTipoCompania);
        }

        public int Agregar(TipoCompania tipoCompania)
        {
            tipoCompaniaValidatorService.ValidarAgregar(tipoCompania);
            tipoCompaniaRepository.Agregar(tipoCompania);
            return tipoCompania.IdTipoCompania;
        }

        public void Editar(TipoCompania tipoCompania)
        {
            tipoCompaniaValidatorService.ValidarEditar(tipoCompania);
            tipoCompaniaRepository.Editar(tipoCompania);
        }

        public void Eliminar(int idTipoCompania)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var tipoCompania = tipoCompaniaRepository.Consultar(idTipoCompania);
                tipoCompaniaValidatorService.ValidarEliminar(idTipoCompania);
                tipoCompaniaRepository.Eliminar(tipoCompania);
                scope.Complete();
            }
        }

    }
}
