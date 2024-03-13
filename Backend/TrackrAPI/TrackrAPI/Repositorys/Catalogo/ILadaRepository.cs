using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ILadaRepository : IRepository<Lada>
    {
        public IEnumerable<LadaDto> ConsultarTodosParaSelector();
    }
}
