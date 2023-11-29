using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    public class ExpedienteConsumoMedicamentoRepository : Repository<TratamientoToma>, IExpedienteConsumoMedicamentoRepository
    {
        public ExpedienteConsumoMedicamentoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<ExpedienteConsumoMedicamentoGridDto> ConsultarParaGrid(int idUsuario)
        {
            DateTime fechaInicioSemanaPasada = DateTime.Today.AddDays(-7);

            return context.TratamientoToma
                .Include(tt => tt.IdTratamientoRecordatorioNavigation)
                .Include(et => et.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation)
                .ThenInclude(tr => tr.IdExpedienteNavigation)
                .Where(tt => tt.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdExpedienteNavigation.IdUsuario == idUsuario
                      && tt.FechaEnvio >= fechaInicioSemanaPasada)
                .Select(x => new ExpedienteConsumoMedicamentoGridDto
                {
                    IdTomaTratamiento = x.IdTomaTratamiento,
                    Farmaco = x.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.Farmaco,
                    Cantidad = x.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.Cantidad,
                    Unidad = x.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.Unidad,
                    Indicaciones = x.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.Indicaciones,
                    Padecimiento = x.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdPadecimientoNavigation.Nombre ?? string.Empty,
                    FechaToma = x.FechaToma,
                    FechaEnvio = x.FechaEnvio,
                }) 
                .ToList();

        }

        public IEnumerable<TratamientoToma> ConsultarConsumoMedicamento(int idUsuario)
        {
            return context.TratamientoToma
                .Include(et => et.IdTratamientoRecordatorioNavigation)
                .Include(et => et.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation)
                .Where(et => et.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdExpedienteNavigation.IdUsuario == idUsuario)
                .ToList();
        }

      public TratamientoToma? ConsularPorNotificacion(int idNotificacion){
            return context.TratamientoToma
            .Include(tt => tt.IdTratamientoRecordatorioNavigation)
            .Include(tt => tt.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation)
            .Include(tt => tt.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdExpedienteNavigation)
            .Where(tt => tt.IdNotificacion == idNotificacion)
            .FirstOrDefault();
      }

    }
}
