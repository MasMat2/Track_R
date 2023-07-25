using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackrAPI.Repositorys.GestionExpediente;
public class ExpedienteTratamientoRepository: Repository<ExpedienteTratamiento>, IExpedienteTratamientoRepository
{
    public ExpedienteTratamientoRepository(TrackrContext context): base(context)
    {
        base.context = context;
    }

    public ExpedienteTratamiento? Consultar(int IdExpedienteTratamiento)
    {
        return context.ExpedienteTratamiento
            .Where(et => et.IdExpedienteTratamiento == IdExpedienteTratamiento)
            .FirstOrDefault();
    }

    public IEnumerable<ExpedienteTratamiento> ConsultarPorUsuario(int idUsuario)
    {
        return context.ExpedienteTratamiento
            .Where(et => et.IdExpedienteNavigation.IdUsuario == idUsuario);
    }
}



