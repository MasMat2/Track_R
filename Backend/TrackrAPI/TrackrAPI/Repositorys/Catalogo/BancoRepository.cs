using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class BancoRepository : Repository<Banco>, IBancoRepository
    {
        public BancoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<BancoDto> ConsultarTodosParaSelector()
        {
            return context.Banco
                .OrderBy(b => b.Nombre)
                .Select(b => new BancoDto
                {
                    IdBanco = b.IdBanco,
                    Nombre = b.Nombre
                })
                .ToList();
        }
    }
}
