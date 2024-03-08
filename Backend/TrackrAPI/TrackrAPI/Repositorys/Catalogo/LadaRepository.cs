using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class LadaRepository : Repository<Lada>, ILadaRepository
    {
        public LadaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<LadaDto> ConsultarTodosParaSelector()
        {
            return context.Lada
                .OrderBy(l => l.Clave)
                .Select(l => new LadaDto
                {
                    IdLada = l.IdLada,
                    ClaveNumero = l.Clave + " " + l.Numero
                })
                .ToList();
        }

    }
}
