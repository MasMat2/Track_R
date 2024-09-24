using Microsoft.EntityFrameworkCore;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo;
public class UnidadMedidaRepository : Repository<UnidadesMedida>, IUnidadesMedidaRepository
{
    public UnidadMedidaRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public async Task<UnidadesMedida?> Consultar(int idEspecialidad)
    {
        return await context.UnidadesMedida
            .Where(es => es.Id == idEspecialidad)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UnidadesMedida>> Consultar()
    {
        return await context.UnidadesMedida.ToListAsync();
    }

    public async Task<UnidadesMedida?> ConsultarPorNombre(string nombre)
    {
        return await context.UnidadesMedida
            .Where(es => es.Nombre.ToLower() == nombre.ToLower())
            .FirstOrDefaultAsync();
    }
}
