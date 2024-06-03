using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;

public interface IExpedienteTratamientoRepository: IRepository<ExpedienteTratamiento>
{
    public IEnumerable<ExpedienteTratamiento> ConsultarParaGrid(int idUsuario);
    public ExpedienteTratamiento ConsultarTratamiento(int idExpedienteTratamiento);
    public IEnumerable<ExpedienteTratamiento> ConsultarTratamientos(int idUsuario);
    public IEnumerable<ExpedienteSelectorDto> SelectorDePadecimiento(int idExpediente);
    public IEnumerable<ExpedienteSelectorDto> SelectorDeDoctor();
    public int Agregar(ExpedienteTratamiento expedienteTratamiento);
    public void AgregarRecordatorios(IEnumerable<TratamientoRecordatorio> recordatorios);
    public void EliminarRecordatorios(IEnumerable<TratamientoRecordatorio> recordatorios);
    public IEnumerable<TratamientoToma> ConsultarTratamientoTomas(int idTratamientoRecordatorio);
    public void EliminarTratamientoTomas(IEnumerable<TratamientoToma> tt);


}

