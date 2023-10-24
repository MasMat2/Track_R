using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;
public interface IExpedienteRecomendacionGeneralRepository : IRepository<ExpedienteRecomendacionesGenerales>
{
    public IEnumerable<ExpedienteRecomendacionGeneralGridDTO> ConsultarGrid();

    public ExpedienteRecomendacionesGenerales? Consultar(int idExpedienteRecomendacionGeneral);
}

