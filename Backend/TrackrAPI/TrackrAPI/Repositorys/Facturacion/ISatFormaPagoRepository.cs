using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Facturacion
{
    public interface ISatFormaPagoRepository : IRepository<SatFormaPago>
    {
        SatFormaPago Consultar(int idSatFormaPago);
        IEnumerable<SatFormaPago> ConsultarParaSelector();
        SatFormaPago ConsultarPorClave(string clave);
    }
}
