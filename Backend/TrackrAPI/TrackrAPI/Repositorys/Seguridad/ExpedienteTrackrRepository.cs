using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class ExpedienteTrackrRepository: Repository<ExpedienteTrackr>, IExpedienteTrackrRepository
    {
        public ExpedienteTrackrRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public ExpedienteTrackr ConsultarPorUsuario(int idUsuario)
        {
            return context.ExpedienteTrackr
                .Include(et => et.ExpedientePadecimiento)
                .Where(et => et.IdUsuario == idUsuario)
                .FirstOrDefault();
        }
    }
}
