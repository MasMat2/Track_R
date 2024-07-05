using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente

{
    public class ExpedienteEstudioRepository : Repository<ExpedienteEstudio>, IExpedienteEstudioRepository
    {
        public ExpedienteEstudioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public ExpedienteEstudio Consultar(int idExpedienteEstudio)
        {
            return context.ExpedienteEstudio
                .Include(ee => ee.IdExpedienteNavigation)
                .Where(ee => ee.IdExpedienteEstudio == idExpedienteEstudio)
                .FirstOrDefault();
        }

        public IEnumerable<ExpedienteEstudioGridDTO> ConsultarPorUsuario(int idUsuario)
        {
            return context.ExpedienteEstudio
                .Where(ee => ee.IdExpedienteNavigation.IdUsuario == idUsuario)
                .Select(ee => new ExpedienteEstudioGridDTO
                {
                    IdExpedienteEstudio = ee.IdExpedienteEstudio,
                    IdExpediente = ee.IdExpediente,
                    FechaRealizacion = ee.FechaRealizacion,
                    Nombre = ee.Nombre,
                    IdArchivo = ee.IdArchivo,
                    UrlArchivo = ee.ArchivoUrl ?? "",
                })
                .ToList();
        }

        public int ConsultarIdExpediente(int idUsuario)
        {
            var idExpediente = context.ExpedienteTrackr
                .Where(ee => ee.IdUsuarioNavigation.IdUsuario == idUsuario)
                .Select(ee => ee.IdExpediente)
                .FirstOrDefault();

            return idExpediente;
        }

    }
}
