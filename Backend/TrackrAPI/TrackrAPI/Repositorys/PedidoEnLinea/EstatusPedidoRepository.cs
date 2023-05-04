using TrackrAPI.Models;
using System.Data;
using TrackrAPI.Dtos.PedidoEnLinea;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public class EstatusPedidoRepository : Repository<EstatusPedido>, IEstatusPedidoRepository
    {
        public EstatusPedidoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public EstatusPedido Consultar(int idEstatusPedido)
        {
            var estatusPedido =
                (from ep in context.EstatusPedido
                 where
                 (ep.IdEstatusPedido == idEstatusPedido)
                 select ep).FirstOrDefault();

            return estatusPedido;
        }

        public EstatusPedido ConsultarPorClave(string clave)
        {
            var estatusPedido =
                (from ep in context.EstatusPedido
                 where
                 (ep.Clave == clave)
                 select ep).FirstOrDefault();

            return estatusPedido;
        }
        public EstatusPedido ConsultarPorNombre(string nombre)
        {
            var estatusPedido =
                (from ep in context.EstatusPedido
                 where
                 (ep.Nombre == nombre)
                 select ep).FirstOrDefault();

            return estatusPedido;
        }

        public IEnumerable<EstatusPedidoSelectorDto> ConsultarTodosParaSelector()
        {
            return
                 context.EstatusPedido
                 .Select(ep => new EstatusPedidoSelectorDto
                 {
                     IdEstatusPedido = ep.IdEstatusPedido,
                     Nombre = ep.Nombre
                 })
                .ToList();
        }
        public IEnumerable<EstatusPedido> ConsultarTodosParaGrid()
        {
            return
                 context.EstatusPedido
                 .Select(ep => new EstatusPedido
                 {
                     IdEstatusPedido = ep.IdEstatusPedido,
                     Nombre = ep.Nombre,
                     Clave = ep.Clave
                 })
                .ToList();
        }

    }
}
