using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Repositorys.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Services.Catalogo
{
    public class LadaService
    {
        private ILadaRepository ladaRepository;

        public LadaService(ILadaRepository ladaRepository)
        {
            this.ladaRepository = ladaRepository;
        }

        public IEnumerable<LadaDto> ConsultarTodosParaSelector()
        {
            return ladaRepository.ConsultarTodosParaSelector();
        }
    }
}
