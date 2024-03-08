using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class SubtipoCuentaContableRepository: Repository<SubtipoCuentaContable> , ISubtipoCuentaContableRepository
    {

        public SubtipoCuentaContableRepository(TrackrContext context) : base(context)
             {
                     base.context = context;
             }

    public IEnumerable<SubtipoCuentaContableDto> ConsultarTodosParaSelector()
    {
        return context.SubtipoCuentaContable
            .OrderBy(scc => scc.Nombre)
            .Select(scc => new SubtipoCuentaContableDto(
                 scc.IdSubtipoCuentaContable,
                 scc.Clave,
                scc.Nombre,
               scc.IdTipoCuentaContable
            ))
            .ToList();
    }

        public IEnumerable<SubtipoCuentaContableDto> ConsultarPorTipoParaSelector(int idTipoCuentaContable)
        {
            return context.SubtipoCuentaContable
                      .Where(e => e.IdTipoCuentaContable == idTipoCuentaContable)
                      .OrderBy(e => e.Nombre)
                      .Select(e => new SubtipoCuentaContableDto(
                          e.IdSubtipoCuentaContable,
                          e.Clave,
                          e.Nombre,
                          e.IdTipoCuentaContable
                       ))
                      .ToList();
        }

        public SubtipoCuentaContableDto ConsultarDto(int idTipoCuentaContable)
        {
            return context.SubtipoCuentaContable
                      .Where(e => e.IdSubtipoCuentaContable == idTipoCuentaContable)
                      .Select(e => new SubtipoCuentaContableDto(
                           e.IdSubtipoCuentaContable,
                          e.Clave,
                          e.Nombre,
                          e.IdTipoCuentaContable))
                      .FirstOrDefault();
        }

        public SubtipoCuentaContable Consultar(int idTipoCuentaContable)
        {
            return context.SubtipoCuentaContable
                      .Where(e => e.IdSubtipoCuentaContable == idTipoCuentaContable)
                      .FirstOrDefault();
        }

    }
}
