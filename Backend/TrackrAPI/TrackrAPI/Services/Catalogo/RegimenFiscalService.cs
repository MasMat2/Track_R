using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Repositorys.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Services.Catalogo
{
    public class RegimenFiscalService
    {
        private IRegimenFiscalRepository regimenFiscalRepository;

        public RegimenFiscalService(IRegimenFiscalRepository regimenFiscalRepository)
        {
            this.regimenFiscalRepository = regimenFiscalRepository;
        }

        public IEnumerable<RegimenFiscalDto> ConsultarTodosParaSelector()
        {
            return regimenFiscalRepository.ConsultarTodosParaSelector();
        }
    }
}
