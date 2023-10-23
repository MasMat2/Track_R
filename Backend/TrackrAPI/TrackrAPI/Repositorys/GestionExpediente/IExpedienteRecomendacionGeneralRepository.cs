using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;
public interface IExpedienteRecomendacionGeneralRepository : IRepository<ExpedienteRecomendacionesGenerales>
{
    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarGrid();
}

