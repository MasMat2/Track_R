using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    public class ExpedienteRecomendacionRepository : Repository<ExpedienteRecomendaciones>, IExpedienteRecomendacionRepository
    {
        public ExpedienteRecomendacionRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<ExpedienteRecomendacionGridDTO> Consultar(int idUsuario)
        {
            return context.ExpedienteRecomendaciones
            .Include(us => us.IdUsuarioDoctorNavigation)
            .Where(er => er.IdExpedienteNavigation.IdUsuario == idUsuario)
            .Select(x => new ExpedienteRecomendacionGridDTO
            {
                IdExpedienteRecomendacion = x.IdExpedienteRecomendaciones,
                Fecha = x.FechaRealizacion.ToShortDateString(),
                Recomendacion = x.Descripcion,
                Doctor = x.IdUsuarioDoctorNavigation.Nombre,
                IdDoctor = x.IdUsuarioDoctor
            })
            .ToList();
        }

        public ExpedienteRecomendaciones? ConsultarPorId(int idExpedienteRecomendacion)
        {
            return context.ExpedienteRecomendaciones
            .Where(er => er.IdExpedienteRecomendaciones == idExpedienteRecomendacion)
            .FirstOrDefault();
        }

    }
}
