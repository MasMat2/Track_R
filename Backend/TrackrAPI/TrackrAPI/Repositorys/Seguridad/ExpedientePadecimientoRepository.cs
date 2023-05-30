using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class ExpedientePadecimientoRepository : Repository<ExpedientePadecimiento>, IExpedientePadecimientoRepository
    {
        public ExpedientePadecimientoRepository(TrackrContext context) : base(context) 
        {
            base.context = context;
        }
        public IEnumerable<ExpedientePadecimientoDTO> ConsultarPorUsuario(int idUsuario)
        {
            return context.ExpedientePadecimiento
                .Where(ep => ep.IdExpedienteNavigation.IdUsuario == idUsuario)
                .Select(ep => new ExpedientePadecimientoDTO
                {
                    IdPadecimiento = ep.IdPadecimiento,
                    FechaDiagnostico = ep.FechaDiagnostico
                }).ToList();
        }

        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarParaSelector()
        {
            return context.ExpedientePadecimiento
                .Select(ep => new ExpedientePadecimientoSelectorDTO
                {
                    IdPadecimiento = ep.IdPadecimiento,
                    Nombre = ep.IdPadecimientoNavigation.Nombre
                }).ToList();
        }
    }
}
