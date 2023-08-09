using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;

namespace TrackrAPI.Repositorys.GestionExpediente;

public class ExpedienteRecomendacionRepository : Repository<ExpedienteRecomendaciones>, IExpedienteRecomendacionRepository
{
    public ExpedienteRecomendacionRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarGridPorUsuario(int idUsuario)
    {
        return context.ExpedienteRecomendaciones
        .Include(us => us.IdUsuarioDoctorNavigation)
        .Where(er => er.IdExpedienteNavigation.IdUsuario == idUsuario)
        .Select(x => new ExpedienteRecomendacionGridDTO
        {
            IdExpedienteRecomendacion = x.IdExpedienteRecomendaciones,
            Fecha = x.FechaRealizacion.ToShortDateString(),
            Descripcion = x.Descripcion,
            Doctor = x.IdUsuarioDoctorNavigation.Nombre
        })
        .ToList();
    }

    public ExpedienteRecomendaciones? Consultar(int idExpedienteRecomendacion)
    {
        return context.ExpedienteRecomendaciones
        .Where(er => er.IdExpedienteRecomendaciones == idExpedienteRecomendacion)
        .FirstOrDefault();
    }

}

