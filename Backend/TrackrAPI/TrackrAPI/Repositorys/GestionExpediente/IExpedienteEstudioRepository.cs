using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    public interface IExpedienteEstudioRepository: IRepository<ExpedienteEstudio>
    {
        public IEnumerable<ExpedienteEstudioGridDTO> ConsultarPorUsuario(int idUsuario);
        public ExpedienteEstudio Consultar(int idExpedienteEstudio);
        public int ConsultarIdExpediente(int idUsuario);

    }
}
