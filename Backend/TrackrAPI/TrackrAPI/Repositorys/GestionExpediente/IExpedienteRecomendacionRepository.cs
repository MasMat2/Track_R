using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;

public interface IExpedienteRecomendacionRepository : IRepository<ExpedienteRecomendaciones>
{
    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarGridPorUsuario(int idUsuario);
    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarGridPorUsuarioRecomendacionGeneral(int idUsuario);
    public ExpedienteRecomendaciones? Consultar(int idExpedienteRecomendacion);
}