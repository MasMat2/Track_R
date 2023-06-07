using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    public class ExpedienteEstudioRepositoy: Repository<ExpedienteEstudio>, IExpedienteEstudioRepository
    {
        public ExpedienteEstudioRepositoy(TrackrContext context): base(context)
        {
            base.context = context;
        }

        public ExpedienteEstudio Consultar(int idExpedienteEstudio)
        {
            return context.ExpedienteEstudio
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
                    Nombre = ee.Nombre
                })
                .ToList();
        }
    }
}
