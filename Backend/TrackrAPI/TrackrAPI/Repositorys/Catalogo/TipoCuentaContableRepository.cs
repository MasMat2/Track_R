using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class TipoCuentaContableRepository : Repository<TipoCuentaContable>, ITipoCuentaContableRepository
    {

        public TipoCuentaContableRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<TipoCuentaContableDto> ConsultarTodosParaGrid()
        {
            return context.TipoCuentaContable
                .Select(tcc => new TipoCuentaContableDto(
                    tcc.IdTipoCuentaContable,
                     tcc.Clave,
                    tcc.Nombre

                ))
                .ToList();
        }

        public IEnumerable<TipoCuentaContableDto> ConsultarTodosParaSelector()
        {
            return context.TipoCuentaContable
                .OrderBy(tcc => tcc.Nombre)
                .Select(tcc => new TipoCuentaContableDto(
                    tcc.IdTipoCuentaContable,
                     tcc.Clave,
                    tcc.Nombre

                ))
                .ToList();
        }

        public TipoCuentaContable ConsultarPorCuentaContable(int idCuentaContable)
        {
            CuentaContable account = context.CuentaContable
                .Include(a => a.IdTipoCuentaContableNavigation)
                .Include(a => a.IdSubtipoCuentaContableNavigation)
                .Where(a => a.IdCuentaContable == idCuentaContable)
                .First();

            return account.IdTipoCuentaContable != null
                ? account.IdTipoCuentaContableNavigation
                : account.IdSubtipoCuentaContableNavigation.IdTipoCuentaContableNavigation;
        }

        public TipoCuentaContable Consultar(int idTipoCuentaContable)
        {
            return context.TipoCuentaContable
                .Where(tc => tc.IdTipoCuentaContable == idTipoCuentaContable)
                .FirstOrDefault();
        }

        public TipoCuentaContableDto ConsultarDto(int idTipoCuentaContable)
        {
            return context.TipoCuentaContable
                .Where(tc => tc.IdTipoCuentaContable == idTipoCuentaContable)
                .Select(tcc => new TipoCuentaContableDto(
                    tcc.IdTipoCuentaContable,
                     tcc.Clave,
                    tcc.Nombre

                ))
                .FirstOrDefault();
        }

        public TipoCuentaContable ConsultarPorNombre(string nombre)
        {
            return context.TipoCuentaContable
                .Where(tc => tc.Nombre.ToLower() == nombre)
                .FirstOrDefault();
        }

        public TipoCuentaContable ConsultarPorClave(string clave)
        {
            return context.TipoCuentaContable
                .Where(tc => tc.Clave.ToLower() == clave)
                .FirstOrDefault();
        }

        public TipoCuentaContable ConsultarDependencias(int idTipoCuentaContable)
        {
            return context.TipoCuentaContable
                .Include(tc => tc.CuentaContable)
                .Include(tc => tc.TipoAuxiliar)
                .Include(tc => tc.SubtipoCuentaContable)
                .Where(tc => tc.IdTipoCuentaContable == idTipoCuentaContable)
                .FirstOrDefault();
        }
    }
}
