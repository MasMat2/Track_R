using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public class AgrupadorCuentaContableRepository : Repository<AgrupadorCuentaContable>, IAgrupadorCuentaContableRepository
    {
        public AgrupadorCuentaContableRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public AgrupadorCuentaContable ConsultarPorCompania(int idCompania)
        {
            return context.Compania
                .Where(c => c.IdCompania == idCompania)
                .FirstOrDefault().IdAgrupadorCuentaContableNavigation;
        }


        public AgrupadorCuentaContable Consultar(int idAgrupadorCuentaContable)
        {
            return context.AgrupadorCuentaContable
                .Where(acc => acc.IdAgrupadorCuentaContable == idAgrupadorCuentaContable)
                .FirstOrDefault();
        }

        public AgrupadorCuentaContable ConsultarPorClave(string clave)
        {
            return context.AgrupadorCuentaContable
                .Where(acc => acc.Clave == clave)
                .FirstOrDefault();
        }

        public AgrupadorCuentaContableDto ConsultarDto(int idAgrupadorCuentaContable)
        {
            AgrupadorCuentaContable agrupador = Consultar(idAgrupadorCuentaContable);

            return new AgrupadorCuentaContableDto()
            {
                IdAgrupadorCuentaContable = agrupador.IdAgrupadorCuentaContable,
                Clave = agrupador.Clave,
                Descripcion = agrupador.Descripcion,
                IdCuentaContableCapital = agrupador.IdCuentaContableCapital,
                IdCuentaContableResultado = agrupador.IdCuentaContableResultado
            };
        }

        public IEnumerable<AgrupadorCuentaContableDto> ConsultarParaGrid()
        {
            return context.AgrupadorCuentaContable
                .Select(acc => new AgrupadorCuentaContableDto()
                {
                    IdAgrupadorCuentaContable = acc.IdAgrupadorCuentaContable,
                    Clave = acc.Clave,
                    Descripcion = acc.Descripcion
                });
        }

        public IEnumerable<AgrupadorCuentaContableDto> ConsultarParaSelector()
        {
            return context.AgrupadorCuentaContable
                .Select(acc => new AgrupadorCuentaContableDto()
                {
                    IdAgrupadorCuentaContable = acc.IdAgrupadorCuentaContable,
                    Clave = acc.Clave,
                    Descripcion = acc.Descripcion
                });
        }

        public AgrupadorCuentaContable ConsultarDependencias(int idAgrupadorCuentaContable)
        {
            return context.AgrupadorCuentaContable
                .Where(acc => acc.IdAgrupadorCuentaContable == idAgrupadorCuentaContable)
                .Include(acc => acc.Compania)
                .Include(acc => acc.CuentaContable)
                .FirstOrDefault();
        }
    }
}
