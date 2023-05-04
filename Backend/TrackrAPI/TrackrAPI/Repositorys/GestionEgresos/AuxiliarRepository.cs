using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionEgresos;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionEgresos
{
    public class AuxiliarRepository : Repository<Auxiliar>, IAuxiliarRepository
    {
        public AuxiliarRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Auxiliar Consultar(int idAuxiliar)
        {
            return context.Auxiliar
                .Where(a => a.IdAuxiliar == idAuxiliar)
                .FirstOrDefault();
        }

        public Auxiliar ConsultarPorDescripcion(string descripcion, int idCompania)
        {
            return context.Auxiliar
                .Where(a => a.IdCompania == idCompania && a.Descripcion == descripcion)
                .FirstOrDefault();
        }

        public Auxiliar ConsultarPorNumero(string numero, int idCompania)
        {
            return context.Auxiliar
                .Where(a => a.IdCompania == idCompania && a.Numero == numero)
                .FirstOrDefault();
        }

        public Auxiliar ConsultarPorNumero(string numero, int idTipoAuxiliar, int idCompania)
        {
            return context.Auxiliar
                .Where(a => a.IdCompania == idCompania && a.Numero == numero && a.IdTipoAuxiliar == idTipoAuxiliar)
                .FirstOrDefault();
        }

        public IEnumerable<AuxiliarSelectorDto> consultarPorTipoAuxiliarParaSelector(int idTipoAuxiliar, int idCompania)
        {
            return context.Auxiliar
                 .Where(a => a.IdTipoAuxiliar == idTipoAuxiliar && a.IdCompania == idCompania)
                 .Select(a => new AuxiliarSelectorDto
                 {
                     IdAuxiliar = a.IdAuxiliar,
                     SelectorLabel = a.Numero + " - " + a.Descripcion,
                     Numero = a.Numero,
                 })
                 .ToList();
        }

        public IEnumerable<AuxiliarSelectorDto> ConsultarParaSelectorVehiculo(int idCompania)
        {
            return context.Auxiliar
                .Where(a => a.IdCompania == idCompania && a.RecibeMovimientos == true)
                .Select(a => new AuxiliarSelectorDto()
                {
                    IdAuxiliar = a.IdAuxiliar,
                    SelectorLabel = a.Numero + " - " + a.Descripcion
                });
        }

        public IEnumerable<AuxiliarSelectorDto> ConsultarPorTipoBalanceParaSelector(int idCompania)
        {
            return context.Auxiliar
                .Where(a => a.IdCompania == idCompania &&
                       a.IdTipoAuxiliarNavigation.IdTipoCuentaContableNavigation.Clave == GeneralConstant.TypeAccountCodeBalance)
                .Select(a => new AuxiliarSelectorDto()
                {
                    IdAuxiliar = a.IdAuxiliar,
                    SelectorLabel = a.Numero + " - " + a.Descripcion
                });
        }

        public IEnumerable<AuxiliarSelectorDto> ConsultarParaSelector(int idCompania)
        {
            return context.Auxiliar
                .Where(a => a.IdCompania == idCompania)
                .Select(a => new AuxiliarSelectorDto()
                {
                    IdAuxiliar = a.IdAuxiliar,
                    Descripcion = a.Descripcion,
                    SelectorLabel = a.Numero + " - " + a.Descripcion
                });
        }

        public IEnumerable<AuxiliarGridDto> consultarTodosParaGrid(int idCompania)
        {
            return context.Auxiliar
                .Include(a => a.IdTipoAuxiliarNavigation)
                .Where(a => a.IdCompania == idCompania)
                .Select(a => new AuxiliarGridDto
                {
                    IdAuxiliar = a.IdAuxiliar,
                    Numero = a.Numero,
                    Descripcion = a.Descripcion,
                    DescripcionTipoAuxiliar = a.IdTipoAuxiliarNavigation.Descripcion,
                    RecibeMovimientosText = a.RecibeMovimientos == true ? "Si" : "No"
                }).ToList();
        }

        public IEnumerable<AuxiliarGridDto> ConsultarParaJerarquiaGrid(int idJerarquia, string clave)
        {
            int tradeCompanyId =
               (from h in context.Jerarquia
                where h.IdJerarquia == idJerarquia
                select h.IdCompania).First();

            // Lista de auxiliares utilizados en la jerarquia.
            var auxiliaryIdList =
                (from h in context.JerarquiaEstructura
                 where h.IdJerarquia == idJerarquia
                 select h.IdAuxiliar);

            var auxiliaryList =
                (from a in context.Auxiliar
                 where a.IdTipoAuxiliarNavigation.Clave == clave
                 && a.IdCompania == tradeCompanyId
                 && !auxiliaryIdList.Contains(a.IdAuxiliar)
                 orderby a.Numero
                 select new AuxiliarGridDto
                 {
                     IdAuxiliar = a.IdAuxiliar,
                     Numero = a.Numero,
                     Descripcion = a.Descripcion,
                     DescripcionTipoAuxiliar = a.IdTipoAuxiliarNavigation.Descripcion,
                     RecibeMovimientosText = a.RecibeMovimientos == true ? "Si" : "No"
                 })
                .OrderBy(a => a.Descripcion)
                .AsQueryable();

            return auxiliaryList.ToList();
        }

        public Auxiliar ConsultarPorJerarquia(int auxiliaryId)
        {
            var auxiliary =
                from hs in context.JerarquiaEstructura
                where hs.IdAuxiliar == auxiliaryId
                && hs.IdJerarquiaNavigation.Estandar
                select hs.IdAuxiliarNavigation;
            return auxiliary.FirstOrDefault();
        }

        public Auxiliar ConsultarDependencias(int idAuxiliar)
        {
            return context.Auxiliar
                .Where(a => a.IdAuxiliar == idAuxiliar)
                .Include(a => a.JerarquiaEstructura)
                .Include(a => a.NotaGastoDetalle)
                .Include(a => a.PolizaAplicadaDetalle)
                .Include(a => a.PolizaDetalle)
                .Include(a => a.Vehiculo)
                .FirstOrDefault();
        }
    }
}
