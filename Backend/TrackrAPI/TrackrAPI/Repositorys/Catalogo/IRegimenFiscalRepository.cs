using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IRegimenFiscalRepository : IRepository<RegimenFiscal>
    {
        public RegimenFiscal Consultar(int idRegimenFiscal);
        public RegimenFiscal Consultar(string clave);
        public IEnumerable<RegimenFiscalDto> ConsultarTodosParaSelector();
    }
}
