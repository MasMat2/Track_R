using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;


namespace TrackrAPI.Repositorys.Catalogo;



public class GeneroRepository : Repository<Genero>, IGeneroRepository
{
    public GeneroRepository(TrackrContext context) : base(context)
    {
    }

    public Genero? Consultar(int idGenero)
    {
        return context.Genero
         .Where(g => g.IdGenero == idGenero)
         .FirstOrDefault();
    }

    public IEnumerable<Genero> Consultar()
    {
        return context.Genero.ToList();
    }


}
