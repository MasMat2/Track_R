using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    
    public class ExpedienteTratamientoRepositoy: Repository<ExpedienteTratamiento>, IExpedienteTratamientoRepository
    {
        public ExpedienteTratamientoRepositoy(TrackrContext context): base(context)
        {
            base.context = context;
        }

        public ExpedienteTratamiento Consultar(int IdExpedienteTratamiento)
        {
            return context.ExpedienteTratamiento
                .Where(et => et.IdExpedienteTratamiento == IdExpedienteTratamiento)
                .FirstOrDefault();
        }

        public IEnumerable<ExpedienteTratamientoGridDTO> ConsultarPorUsuario(int idUsuario)
        {
            return context.ExpedienteTratamiento
                .Where(et => et.IdExpedienteNavigation.IdUsuario == idUsuario)
                .Select(et => new ExpedienteTratamientoGridDTO
                {
                    IdExpedienteTratamiento = et.IdExpedienteTratamiento,
                    Farmaco = et.Farmaco,
                    Cantidad = et.Cantidad,
                    Unidad = et.Unidad,
                    Indicaciones = et.Indicaciones,
                    Padecimiento = et.IdPadecimientoNavigation.Nombre,
                    FechaRegistro = et.FechaRegistro
                })
                .ToList();
        }
    }
}
