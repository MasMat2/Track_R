using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public class PolizaRepository : Repository<Poliza>, IPolizaRepository
    {
        public PolizaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Poliza ConsultarPorIdentificador(string identifier, int tradeCompanyId)
        {
            return
                context.Poliza
                .Where(b => b.Identificador == identifier
                    && b.IdCompania == tradeCompanyId)
                .FirstOrDefault();
        }

        public Poliza ConsultarUltimaPoliza(int idCompania)
        {
            return context.Poliza
                .Where(p => p.IdCompania == idCompania)
                .OrderByDescending(p => p.IdPoliza)
                .FirstOrDefault();
        }

        public Poliza Consultar(int idPoliza)
        {
            return context.Poliza
                .Where(p => p.IdPoliza == idPoliza)
                .FirstOrDefault();
        }

        public IEnumerable<PolizaGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return context.Poliza
                .Include(p => p.IdTipoPolizaNavigation)
                .Include(p => p.IdMonedaNavigation)
                .Where(p => p.IdCompania == idCompania)
                .Select(p => new PolizaGridDto
                {
                    IdPoliza = p.IdPoliza,
                    Numero = p.Numero,
                    Descripcion = p.Descripcion,
                    IdTipoPoliza = p.IdTipoPoliza,
                    DescripcionTipo = p.IdTipoPolizaNavigation.Descripcion,
                    IdMoneda = p.IdMoneda,
                    ClaveMoneda = p.IdMonedaNavigation.Clave,
                    FechaContable = p.FechaContable,
                    FechaMovimiento = p.FechaMovimiento,
                    EsReversa = p.EsReversa,
                    IdPolizaReversa = p.IdPolizaReversa,
                    Origen = p.Origen,
                    IdOrigen = p.IdOrigen
                })
                .OrderByDescending(p => p.IdPoliza)
                .ToList();
        }

        public PolizaDto ConsultarDto(int idPoliza)
        {
            return context.Poliza
                .Include(p => p.IdTipoPolizaNavigation)
                .Include(p => p.IdMonedaNavigation)
                .Include(p => p.PolizaDetalle)
                    .ThenInclude(pd => pd.IdImpuestoNavigation)
                .Where(p => p.IdPoliza == idPoliza)
                .Select(p => new PolizaDto
                {
                    IdPoliza = p.IdPoliza,
                    Numero = p.Numero,
                    Descripcion = p.Descripcion,
                    IdTipoPoliza = p.IdTipoPoliza,
                    DescripcionTipo = p.IdTipoPolizaNavigation.Descripcion,
                    IdMoneda = p.IdMoneda,
                    ClaveMoneda = p.IdMonedaNavigation.Clave,
                    FechaContable = p.FechaContable,
                    FechaMovimiento = p.FechaMovimiento,
                    Origen = p.Origen,
                    IdOrigen = p.IdOrigen,
                    PolizaDetalle = p.PolizaDetalle
                                    .Select(pd => new PolizaDetalleGridDto
                                    {
                                        IdPolizaDetalle = pd.IdPolizaDetalle,
                                        Concepto = pd.Concepto,
                                        Renglon = pd.Renglon,
                                        Cargo = pd.Cargo,
                                        Abono = pd.Abono,
                                        IdCuentaContable = pd.IdCuentaContable,
                                        CuentaContable = pd.IdCuentaContableNavigation.Nombre,
                                        IdAuxiliar = pd.IdAuxiliar,
                                        DescripcionAuxiliar = pd.IdAuxiliarNavigation.Descripcion
                                    }).ToList()
                })
                .FirstOrDefault();
        }

        public bool TienePolizasGeneradas(int idCompania)
        {
            return context.Poliza.Any(p => p.IdCompania == idCompania);
        }

        public Poliza ConsultarPorNumero(string numero)
        {
            return context.Poliza
                .Where(p => p.Numero == numero)
                .FirstOrDefault();
        }

        public IEnumerable<PolizaGridDto> ConsultarFiltroParaGrid(PolizaFiltroDto filtro)
        {
            return context.Poliza
                .Include(p => p.IdTipoPolizaNavigation)
                .Include(p => p.IdMonedaNavigation)
                .Where(p => p.IdCompania == filtro.IdCompania)
                .Where(p =>
                      (filtro.Numero == "undefined" || string.IsNullOrEmpty(filtro.Numero) || p.Numero == filtro.Numero) &&
                      (filtro.IdTipoPoliza == 0 || p.IdTipoPoliza == filtro.IdTipoPoliza) &&
                      (filtro.FechaContable == null || ((DateTime)p.FechaContable).Date == ((DateTime)filtro.FechaContable).Date) &&
                      (filtro.FechaMovimiento == null || ((DateTime)p.FechaMovimiento).Date == ((DateTime)filtro.FechaMovimiento).Date))
                .Select(p => new PolizaGridDto
                {
                    IdPoliza = p.IdPoliza,
                    Numero = p.Numero,
                    Descripcion = p.Descripcion,
                    IdTipoPoliza = p.IdTipoPoliza,
                    DescripcionTipo = p.IdTipoPolizaNavigation.Descripcion,
                    IdMoneda = p.IdMoneda,
                    ClaveMoneda = p.IdMonedaNavigation.Clave,
                    FechaContable = p.FechaContable,
                    FechaMovimiento = p.FechaMovimiento,
                    EsReversa = p.EsReversa,
                    IdPolizaReversa = p.IdPolizaReversa,
                    Origen = p.Origen,
                    IdOrigen = p.IdOrigen
                })
                .OrderByDescending(p => p.IdPoliza)
                .ToList();
        }


        public Poliza ConsultarPorDocumentoOrigen(int idDocumentoOrigen, string origen)
        {
            return context.Poliza
                .Where(p => p.IdOrigen == idDocumentoOrigen && p.Origen == origen && !p.EsReversa)
                .FirstOrDefault();
        }
    }
}
