using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
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
                    IdExpedientePadecimiento = ep.IdExpedientePadecimiento,
                    IdPadecimiento = ep.IdPadecimiento,
                    FechaDiagnostico = ep.FechaDiagnostico
                }).ToList();
        }

        public IEnumerable<ExpedientePadecimientoGridDTO> ConsultarParaGridPorUsuario(int idUsuario)
        {
            return context.ExpedientePadecimiento
                .Where(ep => ep.IdExpedienteNavigation.IdUsuario == idUsuario)
                .Select(ep => new ExpedientePadecimientoGridDTO
                {
                    IdExpedientePadecimiento = ep.IdExpedientePadecimiento,
                    IdPadecimiento = ep.IdPadecimiento,
                    FechaDiagnostico = ep.FechaDiagnostico,
                    NombrePadecimiento = ep.IdPadecimientoNavigation.Nombre,
                    NombreDoctor = ep.IdUsuarioDoctorNavigation.ObtenerNombreCompleto()

                });
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

        public void EliminarPorExpediente(int idExpediente)
        {
            var padecimientos = context.ExpedientePadecimiento.Where(ep => ep.IdExpediente == idExpediente);
            context.ExpedientePadecimiento.RemoveRange(padecimientos);
        }
    }
}
