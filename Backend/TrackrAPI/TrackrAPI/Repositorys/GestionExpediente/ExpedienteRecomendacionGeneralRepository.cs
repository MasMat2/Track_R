using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;
public class ExpedienteRecomendacionGeneralRepository : Repository<ExpedienteRecomendacionesGenerales>, IExpedienteRecomendacionGeneralRepository
{
    public ExpedienteRecomendacionGeneralRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarGrid()
    {
        return context.ExpedienteRecomendacionesGenerales
        .Include(us => us.IdAdministradorNavigation)
        .Select(x => new ExpedienteRecomendacionGridDTO
        {
            IdExpedienteRecomendacion = x.IdExpedienteRecomendacionesGenerales,
            Fecha = x.FechaRealizacion.ToShortDateString(),
            Descripcion = x.Descripcion,
            Doctor = x.IdAdministradorNavigation.Nombre
        })
        .ToList();
    }
}

