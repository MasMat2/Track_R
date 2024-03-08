using System.Transactions;
using TrackrAPI.Dtos.PedidoEnLinea;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.PedidoEnLinea;

namespace TrackrAPI.Services
{
    public class CarritoService
    {
        private readonly ICarritoRepository carritoRepository;

        public CarritoService(ICarritoRepository carritoRepository)
        {
            this.carritoRepository = carritoRepository;
        }

        public List<CarritoDto> ConsultarPorUsuarioComprador(int idUsuarioComprador, string token)
        {
            return carritoRepository.ConsultarPorUsuarioComprador(idUsuarioComprador, token);
        }

        public double ConsultarAgregadoPorToken(string token)
        {
            return carritoRepository.ConsultarAgregadoPorToken(token);
        }

        public int Agregar(CarritoDto itemDto, int idUsuario, string token)
        {
            var carritoActual = carritoRepository.ConsultarPorUsuarioComprador(idUsuario, token);
            var itemEnCarrito = carritoActual.Find(x => x.IdPresentacion == itemDto.IdPresentacion);

            if (itemEnCarrito != null)
            {
                itemEnCarrito.Cantidad += (double)itemDto.Cantidad;
                Editar(itemEnCarrito);

                return itemEnCarrito.IdCarrito;
            }
            else
            {
                var carrito = new Carrito
                {
                    IdPresentacion = itemDto.IdPresentacion,
                    IdUsuarioComprador = idUsuario <= 0 ? null : idUsuario,
                    Cantidad = (decimal)itemDto.Cantidad,
                    Comentarios = itemDto.Comentarios,
                    IdCompania = itemDto.IdCompania,
                    Token = idUsuario > 0 ? null : token
                };

                return carritoRepository.Agregar(carrito).IdCarrito;
            }
        }

        public void Editar(CarritoDto carritoDto)
        {
            Carrito carrito = carritoRepository.Consultar(carritoDto.IdCarrito);

            if (carrito == null)
            {
                throw new CdisException("El producto no se encuentra en el carrito.");
            }

            carrito.Cantidad = (decimal)carritoDto.Cantidad;
            carrito.Comentarios = carritoDto.Comentarios;

            carritoRepository.Editar(carrito);
        }

        public void Eliminar(int idCarrito)
        {
            Carrito carrito = carritoRepository.Consultar(idCarrito);
            carritoRepository.Eliminar(carrito);
        }

        public void VaciarCarrito(int idUsuario, string token)
        {
            var carrito = carritoRepository.ConsultarPorUsuarioComprador(idUsuario, token);

            using var ts = new TransactionScope();

            foreach (var item in carrito)
            {
                Eliminar(item.IdCarrito);
            }

            ts.Complete();
        }

        public void ActualizarCarrito(List<CarritoDto> nuevoCarrito, int idUsuario, string token)
        {
            var carritoActual = carritoRepository.ConsultarPorUsuarioComprador(idUsuario, token);

            static bool IsItemEqual(CarritoDto x, CarritoDto y)
            {
                return x.IdPresentacion == y.IdPresentacion;
            }

            using var ts = new TransactionScope();

            // Items del nuevo carrito que no estén en el carrito actual
            var nuevosItems = nuevoCarrito.ExceptBy(carritoActual, IsItemEqual);

            foreach (var item in nuevosItems)
            {
                Agregar(item, idUsuario, token);
            }

            // Items del carrito actual qu eno estén en el nuevo carrito
            var itemsEliminados = carritoActual.ExceptBy(nuevoCarrito, IsItemEqual);

            foreach (var item in itemsEliminados)
            {
                Eliminar(item.IdCarrito);
            }

            // Items que estén en ambos carritos y presenten algún cambio
            var itemsActualizados = nuevoCarrito
                .ExceptBy(nuevosItems, IsItemEqual)
                .ExceptBy(itemsEliminados, IsItemEqual)
                .Where((x) => carritoActual.Any((y) =>
                    y.IdPresentacion == x.IdPresentacion &&
                    (y.Cantidad != x.Cantidad || y.Comentarios != x.Comentarios)
                ));

            foreach (var item in itemsActualizados)
            {
                Editar(item);
            }

            ts.Complete();
        }
    }
}
