using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class CuentaContableRepository : Repository<CuentaContable>, ICuentaContableRepository
    {
        public CuentaContableRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<CuentaContableDto> ConsultarTodosParaSelector(int idCompania)
        {
            return context.CuentaContable
                .Where(cc => cc.IdCompania == idCompania)
                .Select(cc => new CuentaContableDto
                {
                    IdCuentaContable = cc.IdCuentaContable,
                    Numero = cc.Numero,
                    Nombre = cc.Nombre,
                    Descripcion = cc.Descripcion,
                    Reconciliatoria = cc.Reconciliatoria,
                    RecibeMovimientos = cc.RecibeMovimientos,
                    Auxiliar = cc.Auxiliar,
                    PartidaAbierta = cc.PartidaAbierta,
                    Automatica = cc.Automatica,
                    NumeroNombre = cc.NumeroNombre(),
                    IdSubtipoCuentaContable = cc.IdSubtipoCuentaContable,
                    IdTipoCuentaContable = cc.IdTipoCuentaContable,
                    IdTipoAuxiliar = cc.IdTipoAuxiliar,
                    EsConcepto = cc.EsConcepto,
                    IdCuentaContablePadre = cc.IdCuentaContablePadre,
                    IdAgrupadorCuentaContable = cc.IdAgrupadorCuentaContable
                })
                .OrderBy(c => c.Numero)
                .ToList();
        }

        public IEnumerable<CuentaContableDto> ConsultarPorAgrupadorParaSelector(int idAgrupador)
        {
            return context.CuentaContable
                .Where(cc => cc.IdAgrupadorCuentaContable == idAgrupador)
                .Select(cc => new CuentaContableDto
                {
                    IdCuentaContable = cc.IdCuentaContable,
                    Numero = cc.Numero,
                    Nombre = cc.Nombre,
                    Descripcion = cc.Descripcion,
                    Reconciliatoria = cc.Reconciliatoria,
                    RecibeMovimientos = cc.RecibeMovimientos,
                    Auxiliar = cc.Auxiliar,
                    PartidaAbierta = cc.PartidaAbierta,
                    Automatica = cc.Automatica,
                    NumeroNombre = cc.NumeroNombre(),
                    IdSubtipoCuentaContable = cc.IdSubtipoCuentaContable,
                    IdTipoCuentaContable = cc.IdTipoCuentaContable,
                    IdTipoAuxiliar = cc.IdTipoAuxiliar,
                    EsConcepto = cc.EsConcepto,
                    IdCuentaContablePadre = cc.IdCuentaContablePadre,
                    IdAgrupadorCuentaContable = cc.IdAgrupadorCuentaContable
                })
                .OrderBy(c => c.Numero)
                .ToList();
        }

        public IEnumerable<CuentaContable> ConsultarPorCompaniaBaseYAgrupador(int idAgrupador)
        {
            int? idCompaniaBase = context.Compania.FirstOrDefault(cc => cc.Clave == GeneralConstant.ClaveCompaniaBase)?.IdCompania;

            return context.CuentaContable
                .Where(cc => cc.IdAgrupadorCuentaContable == idAgrupador && cc.IdCompania == idCompaniaBase)
                .ToList();
        }

        public IEnumerable<CuentaContableGridDto> ConsultarPorAgrupadorParaGrid(int idAgrupador)
        {
            int? idCompaniaBase = context.Compania.FirstOrDefault(cc => cc.Clave == GeneralConstant.ClaveCompaniaBase)?.IdCompania;

            return context.CuentaContable
                .Where(cc => cc.IdAgrupadorCuentaContable == idAgrupador && cc.IdCompania == idCompaniaBase)
                .Select(cc => new CuentaContableGridDto
                {
                    IdCuentaContable = cc.IdCuentaContable,
                    Numero = cc.Numero,
                    Nombre = cc.Nombre,
                    Descripcion = cc.Descripcion,
                    Reconciliatoria = cc.Reconciliatoria,
                    RecibeMovimientos = cc.RecibeMovimientos,
                    Auxiliar = cc.Auxiliar,
                    PartidaAbierta = cc.PartidaAbierta,
                    Automatica = cc.Automatica,
                    TipoCuentaNombre = cc.IdTipoCuentaContableNavigation.Nombre,
                    SubtipoCuentaNombre= cc.IdSubtipoCuentaContableNavigation.Nombre,
                    IdSubtipoCuentaContable = cc.IdSubtipoCuentaContable,
                    IdTipoCuentaContable = cc.IdTipoCuentaContable,
                    IdCompania = cc.IdCompania
                })
                .ToList();
        }

        public CuentaContable Consultar(int idCuentaContable)
        {
            return context.CuentaContable
                    .Include(cc => cc.IdSubtipoCuentaContableNavigation)
                .Where(cc => cc.IdCuentaContable == idCuentaContable).FirstOrDefault();
        }

        public CuentaContableDto ConsultarDto(int idCuentaContable)
        {
            return context.CuentaContable
                .Where(cc => cc.IdCuentaContable == idCuentaContable)
                .Select(cc => new CuentaContableDto {
                    IdCuentaContable = cc.IdCuentaContable,
                    Numero = cc.Numero,
                    Nombre = cc.Nombre,
                    Descripcion = cc.Descripcion,
                    Reconciliatoria = cc.Reconciliatoria,
                    RecibeMovimientos = cc.RecibeMovimientos,
                    Auxiliar = cc.Auxiliar,
                    PartidaAbierta = cc.PartidaAbierta,
                    Automatica = cc.Automatica,
                    NumeroNombre = cc.NumeroNombre(),
                    IdSubtipoCuentaContable = cc.IdSubtipoCuentaContable,
                    IdTipoCuentaContable = cc.IdTipoCuentaContable,
                    IdTipoAuxiliar = cc.IdTipoAuxiliar,
                    EsConcepto = cc.EsConcepto,
                    IdCuentaContablePadre = cc.IdCuentaContablePadre,
                    IdAgrupadorCuentaContable = cc.IdAgrupadorCuentaContable
                })
                .FirstOrDefault();
        }

        public IEnumerable<CuentaContableGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return context.CuentaContable
                .Where(cc => cc.IdCompania == idCompania)
                .OrderBy(cc => cc.Numero)
                .Select(cc => new CuentaContableGridDto (
                    cc.IdCuentaContable,
                    cc.Numero,
                    cc.Nombre,
                    cc.Descripcion,
                    cc.Reconciliatoria,
                    cc.RecibeMovimientos,
                    cc.Auxiliar,
                    cc.PartidaAbierta,
                    cc.Automatica,
                    cc.IdTipoCuentaContableNavigation.Nombre,
                    cc.IdSubtipoCuentaContableNavigation.Nombre,
                    cc.IdSubtipoCuentaContable,
                    cc.IdTipoCuentaContable,
                    cc.IdCompania
                ))
                .ToList();
        }

        public CuentaContable ConsultarPorNumero(int idCompania, string numero)
        {
            return context.CuentaContable
                    .Include(e=> e.IdTipoAuxiliarNavigation)
                .Where(e => e.Numero == numero
                    && e.IdCompania == idCompania)
                .FirstOrDefault();
        }

        public CuentaContable ConsultarPorNombre(int idCompania, string nombre)
        {
            return context.CuentaContable
                .Where(e => e.Nombre.ToLower() == nombre.ToLower()
                    && e.IdCompania == idCompania)
                .FirstOrDefault();
        }

        public CuentaContable ConsultarDependencias(int idCuentaContable)
        {
            return context.CuentaContable
                .Include(e => e.IdAgrupadorCuentaContableNavigation)
                .Include(e => e.Almacen)
                .Include(e => e.BalanceCuentaContable)
                .Include(e => e.CajaIdCuentaContableAutomaticaNavigation)
                .Include(e => e.CajaIdCuentaContableNavigation)
                .Include(e => e.Concepto)
                .Include(e => e.Impuesto)
                .Include(e => e.ImpuestoDetalleIdCuentaContableAbonoNavigation)
                .Include(e => e.ImpuestoDetalleIdCuentaContableCargoNavigation)
                .Include(e => e.InverseIdCuentaContablePadreNavigation)
                .Include(e => e.JerarquiaEstructura)
                .Include(e => e.PolizaAplicadaDetalle)
                .Include(e => e.PolizaDetalle)
                .Include(e => e.TipoActivo)
                .Include(e => e.UsuarioRol)

                .Where(e => e.IdCuentaContable == idCuentaContable)
                .FirstOrDefault();
        }

        public IEnumerable<CuentaContable> ConsultarParaJerarquiaGrid(int idJerarquia)
        {
            // Lista de cuentas utilizadas en la jerarquia.
            var accountIdList =
                (from h in context.JerarquiaEstructura
                 where h.IdJerarquia == idJerarquia
                 select h.IdCuentaContable);

            // Compañia a la que pertenece la jerarquia.
            int tradeCompanyId =
                (from h in context.Jerarquia
                 where h.IdJerarquia == idJerarquia
                 select h.IdCompania).First();

            var accountList =
                (from a in context.CuentaContable
                 where a.IdCompania == tradeCompanyId
                 && !accountIdList.Contains(a.IdCuentaContable)
                 select a);

            return accountList.ToList();
        }

        public IEnumerable<CuentaPartidaVivaGridDto> ConsultarConPartidasAbiertas(int idCompania)
        {

            var openEntryIds = (
               from p in context.PolizaAplicadaDetalle
               where p.IdPartidaViva != null
               && p.IdPolizaDetalleNavigation.IdPolizaNavigation.IdCompania == idCompania
               select p.IdPartidaViva);

            var accountList =
                (from pad in context.PolizaAplicadaDetalle
                 join balanceAccount in context.BalanceCuentaContable on
                 new
                 {
                     joinAccountId = (int)pad.IdCuentaContable,
                     joinTradeCompanyId = (int)pad.IdPolizaAplicadaNavigation.IdPolizaNavigation.IdCompania,
                     joinMonth = pad.IdPolizaAplicadaNavigation.IdPolizaNavigation.FechaContable.Month,
                     joinYear = pad.IdPolizaAplicadaNavigation.IdPolizaNavigation.FechaContable.Year
                 }
                 equals
                 new
                 {
                     joinAccountId = balanceAccount.IdCuentaContable,
                     joinTradeCompanyId = balanceAccount.IdCompania,
                     joinMonth = balanceAccount.Mes,
                     joinYear = balanceAccount.Anio
                 }
                 where pad.IdPolizaAplicadaNavigation.IdPolizaNavigation.IdCompania == idCompania
                 && pad.EstaViva
                 && pad.IdCuentaContableNavigation.PartidaAbierta
                 && (!openEntryIds.Contains(pad.IdPolizaAplicadaDetalle))
                 select new CuentaPartidaVivaGridDto
                 {
                     IdCuentaContable = pad.IdCuentaContable,
                     Descripcion = pad.IdCuentaContableNavigation.Numero + GeneralConstant.DescriptionSeparator + pad.IdCuentaContableNavigation.Descripcion,
                     Saldo = ((balanceAccount.Cargo) - (balanceAccount.Abono)),
                     Mes = balanceAccount.Mes,
                     Anio = balanceAccount.Anio
                 }).Distinct().OrderBy(a => a.IdCuentaContable);

            return accountList;
        }

        public CuentaContable ConsultarPorJerarquia(int idCuentaContable, int idCompania)
        {
            var account =
                from hs in context.JerarquiaEstructura
                where hs.IdCuentaContable == idCuentaContable
                && hs.IdJerarquiaNavigation.IdCompania == idCompania
                && hs.IdJerarquiaNavigation.Estandar
                select hs.IdCuentaContableNavigation;
            return account.FirstOrDefault();
        }

        public IEnumerable<CuentaContableGridDto> ConsultarPorFiltroParaGrid(int idCompania, CuentaContableFiltroDto filtro)
        {
            return context.CuentaContable
                    .Where(cc => cc.IdCompania == idCompania
                                && (string.IsNullOrEmpty(filtro.Nombre) || cc.Nombre.ToLower().Contains(filtro.Nombre))
                                && (string.IsNullOrEmpty(filtro.Numero) || cc.Numero.ToLower().Contains(filtro.Numero))
                                && (cc.IdTipoCuentaContable == filtro.IdTipoCuentaContable || filtro.IdTipoCuentaContable == 0))
                    .OrderBy(cc => cc.Numero)
                    .Select(cc => new CuentaContableGridDto(
                        cc.IdCuentaContable,
                        cc.Numero,
                        cc.Nombre,
                        cc.Descripcion,
                        cc.Reconciliatoria,
                        cc.RecibeMovimientos,
                        cc.Auxiliar,
                        cc.PartidaAbierta,
                        cc.Automatica,
                        cc.IdTipoCuentaContableNavigation.Nombre,
                        cc.IdSubtipoCuentaContableNavigation.Nombre,
                        cc.IdSubtipoCuentaContable,
                        cc.IdTipoCuentaContable,
                        cc.IdCompania
                    ))
                    .ToList();
        }
    }
}
