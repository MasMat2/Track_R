using Microsoft.EntityFrameworkCore;
using Trackr.Dtos.PedidoEnLinea;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public class FlujoRepository : Repository<Flujo>, IFlujoRepository
    {
        public FlujoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<FlujoDto> ConsultarTodosParaSelector(int idCompania)
        {
            return context.Flujo
                .Where(f => f.IdCompania == idCompania)
                .OrderBy(f => f.Clave)
                .Select(f => new FlujoDto()
                {
                    IdFlujo = f.IdFlujo,
                    Clave = f.Clave,
                    Nombre = f.Nombre
                });
        }

        public IEnumerable<FlujoDto> ConsultarTodosParaGrid(int idCompania, int idUsuario)
        {
            var idsRoles = context.UsuarioRol.Where(ur => ur.IdUsuario == idUsuario).Select(ur => ur.IdRol).ToList();

            return context.Flujo
                .Where(f => f.IdCompania == idCompania)
                .OrderBy(f => f.Clave)
                .Select(f => new FlujoDto()
                {
                    IdFlujo = f.IdFlujo,
                    Clave = f.Clave,
                    Nombre = f.Nombre,
                    EsDefault = f.EsDefault,
                    PermiteModificar = f.IdRol == null || idsRoles.Contains((int)f.IdRol)
                });
        }

        public Flujo Consultar(int idFlujo)
        {
            return context.Flujo
                .Where(f => f.IdFlujo == idFlujo)
                .FirstOrDefault();
        }

        public Flujo ConsultarPorClave(int idCompania, string clave)
        {
            return context.Flujo
                .Where(f => f.IdCompania == idCompania && f.Clave == clave)
                .FirstOrDefault();
        }

        public Flujo ConsultarDefault(int idCompania)
        {
            return context.Flujo
                .Where(f => f.IdCompania == idCompania && f.EsDefault == true)
                .FirstOrDefault();
        }

        public Flujo ConsultarPorPresentacionOpcionVenta(int idPresentacion, int idCompania, string claveOpcionVenta)
        {
            int idFlujo;
            var configuracionOpcionVenta = context.ConfiguracionOpcionVenta
                .Include(c => c.IdOpcionVentaNavigation)
                .Where(c => c.IdPresentacion == idPresentacion && c.IdOpcionVentaNavigation.Clave == claveOpcionVenta)
                .FirstOrDefault();

            if (configuracionOpcionVenta != null)
            {
                idFlujo = configuracionOpcionVenta.IdFlujo;
            }
            else // Si la presentación no cuenta con la configuración de opcion de venta
            {
                // Se consulta el flujo estándar default de la compañía
                idFlujo = context.Flujo.Where(f => f.IdCompania == idCompania && f.EsDefault == true)
                          .FirstOrDefault().IdFlujo;
            }

            return context.Flujo
                .Where(f => f.IdFlujo == idFlujo)
                .FirstOrDefault();
        }

        public Flujo ConsultarDependencias(int idFlujo)
        {
            return context.Flujo
                .Include(f => f.Presentacion)
                .Include(f => f.ConfiguracionOpcionVenta)
                .Where(f => f.IdFlujo == idFlujo)
                .FirstOrDefault();
        }
    }
}
