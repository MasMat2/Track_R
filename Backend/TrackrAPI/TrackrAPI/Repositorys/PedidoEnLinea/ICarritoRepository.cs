using TrackrAPI.Dtos.PedidoEnLinea;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public interface ICarritoRepository : IRepository<Carrito>
    {
        List<CarritoDto> ConsultarPorUsuarioComprador(int idUsuarioComprador, string token);
        Carrito Consultar(int idCarrito);
        double ConsultarAgregadoPorToken(string token);
    }
}
