using Trackr.Dtos.PedidoEnLinea;
using TrackrAPI.Models;
using TrackrAPI.Repositorys;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public interface IFlujoRepository : IRepository<Flujo>
    {
        IEnumerable<FlujoDto> ConsultarTodosParaSelector(int idCompania);
        IEnumerable<FlujoDto> ConsultarTodosParaGrid(int idCompania, int idUsuario);
        Flujo ConsultarPorPresentacionOpcionVenta(int idPresentacion, int idCompania, string claveOpcionVenta);
        Flujo Consultar(int idFlujo);
        Flujo ConsultarPorClave(int idCompania, string clave);
        Flujo ConsultarDefault(int idCompania);
        Flujo ConsultarDependencias(int idFlujo);
    }
}