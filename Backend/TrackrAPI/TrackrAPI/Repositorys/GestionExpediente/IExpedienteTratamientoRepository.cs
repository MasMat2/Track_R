using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;
public interface IExpedienteTratamientoRepository: IRepository<ExpedienteTratamiento>
{
    public IEnumerable<ExpedienteTratamiento> ConsultarParaGrid(int idUsuario);
    public ExpedienteTratamiento? Consultar(int idExpedienteTratamiento);
    public IEnumerable<ExpedienteTratamiento> ConsultarPorUsuario(int idUsuario);
    public IEnumerable<ExpedienteSelectorDto> SelectorDePadecimiento(int idExpediente);
    public IEnumerable<ExpedienteSelectorDto> SelectorDeDoctor();
    public int Agregar(ExpedienteTratamiento expedienteTratamiento);
    public void AgregarRecordatorios(IEnumerable<TratamientoRecordatorio> recordatorios);

}

