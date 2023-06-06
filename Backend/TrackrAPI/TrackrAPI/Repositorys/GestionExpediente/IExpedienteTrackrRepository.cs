using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    public interface IExpedienteTrackrRepository : IRepository<ExpedienteTrackr>
    {
        public ExpedienteTrackr Consultar(int idExpediente);
        public ExpedienteTrackr ConsultarPorNumero(string numero);
        public ExpedienteTrackr ConsultarPorUsuario(int idUsuario);

        public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaGrid();

    }
}
