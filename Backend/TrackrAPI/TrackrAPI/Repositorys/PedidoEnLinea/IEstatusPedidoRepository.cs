using TrackrAPI.Dtos.PedidoEnLinea;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public interface IEstatusPedidoRepository : IRepository<EstatusPedido>
    {
        EstatusPedido Consultar(int idEstatusPedido);
        EstatusPedido ConsultarPorClave(string clave);
        EstatusPedido ConsultarPorNombre(string nombre);
        IEnumerable<EstatusPedidoSelectorDto> ConsultarTodosParaSelector();
        IEnumerable<EstatusPedido> ConsultarTodosParaGrid();
    }
}
