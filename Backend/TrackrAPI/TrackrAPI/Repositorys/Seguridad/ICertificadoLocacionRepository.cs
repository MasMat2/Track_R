using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface ICertificadoLocacionRepository : IRepository<CertificadoLocacion>
    {
        CertificadoLocacion ConsultarPorLocacion(int idLocacion);
    }
}
