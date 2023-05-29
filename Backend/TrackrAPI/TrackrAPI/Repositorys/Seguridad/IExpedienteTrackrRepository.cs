using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IExpedienteTrackrRepository : IRepository<ExpedienteTrackr>
    {
        public ExpedienteTrackr  ConsultarPorUsuario(int idUsuario);

    }
}
