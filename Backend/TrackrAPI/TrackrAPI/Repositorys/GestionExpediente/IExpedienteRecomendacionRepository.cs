using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;

public interface IExpedienteRecomendacionRepository : IRepository<ExpedienteRecomendaciones>
{
    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarPorUsuario(int idUsuario);
    public ExpedienteRecomendaciones? ConsultarPorId(int idExpedienteRecomendacion);
}