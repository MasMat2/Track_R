using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Dtos.Genero;

namespace TrackrAPI.Repositorys.Genero
{


    public class GeneroRepository : Repository<Genero>
    {
        public GeneroRepository(TrackrContext context) : base(context)
        {
        }

        public Genero? ConsultarDto(int idUsuario)
        {
            return context.Genero
             .Where(g => g.IdUsuario == idUsuario)
             .Include(g => g.TipoDeGenero)
             .FirstOrDefault();
        }
        public IEnumerable<GeneroDto> ConsultarPorTipoDeGenero(string tipoDeGenero)
        {
            
        }
    }

}
