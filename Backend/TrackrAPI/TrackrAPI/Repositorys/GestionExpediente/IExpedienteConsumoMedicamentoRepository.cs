using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    public interface IExpedienteConsumoMedicamentoRepository: IRepository<TratamientoToma>
    {
        public IEnumerable<ExpedienteConsumoMedicamentoGridDto> ConsultarParaGrid(int idUsuario);
        public IEnumerable<TratamientoToma> ConsultarConsumoMedicamento(int idUsuario);
         public TratamientoToma? ConsularPorNotificacion(int idNotificacion);
    }
}
