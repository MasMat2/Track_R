using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;
public interface IExpedienteTratamientoRepository: IRepository<ExpedienteTratamiento>
{
    public IEnumerable<ExpedienteTratamiento> ConsultarPorUsuario(int idUsuario);
    
    public ExpedienteTratamiento? Consultar(int idExpedienteTratamiento);
}

