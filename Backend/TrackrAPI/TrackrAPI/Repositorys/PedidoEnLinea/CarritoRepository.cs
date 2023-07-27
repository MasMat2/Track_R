using System.Data;
using TrackrAPI.Dtos.PedidoEnLinea;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public class CarritoRepository : Repository<Carrito>, ICarritoRepository
    {
        public CarritoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Carrito Consultar(int idCarrito)
        {
            var caja = from c in context.Carrito
                       where c.IdCarrito == idCarrito
                       select c;
            return caja.FirstOrDefault();
        }

        public List<CarritoDto> ConsultarPorUsuarioComprador(int idUsuarioComprador, string token)
        {
            var hoy = Utileria.ObtenerFechaActual();

            var carritoList = context.Carrito
                .Where(c => c.IdUsuarioComprador == idUsuarioComprador || c.Token == token)
                .Select(c => new CarritoDto
                {
                    IdCarrito = c.IdCarrito,
                    IdPresentacion = c.IdPresentacion,
                    IdCompania = c.IdCompania,
                    Cantidad = (double)c.Cantidad,
                    NombrePresentacion = c.IdPresentacionNavigation.Nombre,
                    DescripcionPresentacion = c.IdPresentacionNavigation.Descripcion,
                    ImagenPresentacion = c.IdPresentacionNavigation.PresentacionImagen.Any()
                        ? c.IdPresentacionNavigation.PresentacionImagen.FirstOrDefault().Imagen
                        : null,
                    Precio = (double)c.IdPresentacionNavigation.ListaPrecioDetalle
                        .FirstOrDefault(plp =>
                            plp.IdPresentacion == c.IdPresentacionNavigation.IdPresentacion
                            && hoy.Date >= plp.IdListaPrecioNavigation.FechaInicioVigencia.Date
                            && hoy.Date <= plp.IdListaPrecioNavigation.FechaFinVigencia.Date
                        )
                        .PrecioBase
                });

            return carritoList.ToList();
        }

        public double ConsultarAgregadoPorToken(string token)
        {
            var total =
                (from c in context.Carrito
                 where c.Token == token
                 select c.Cantidad
                 ).ToList().DefaultIfEmpty(0).Sum(cantidad => cantidad);

            return (double)total;
        }

    }
}
