using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public class JerarquiaEstructuraRepository : Repository<JerarquiaEstructura>, IJerarquiaEstructuraRepository
    {
        public JerarquiaEstructuraRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public JerarquiaEstructura ConsultarPorCuentaContable(int hierarchyId, int accountId)
        {
            var JerarquiaEstructura =
                from h in context.JerarquiaEstructura
                where h.IdJerarquia == hierarchyId
                && h.IdCuentaContable == accountId
                orderby h.IdCuentaContableNavigation.Numero
                select h;
            return JerarquiaEstructura.FirstOrDefault();
        }

        public JerarquiaEstructura ConsultarPorAuxiliar(int hierarchyId, int auxiliaryId)
        {
            var JerarquiaEstructura =
                from h in context.JerarquiaEstructura
                where h.IdJerarquia == hierarchyId
                && h.IdAuxiliar == auxiliaryId
                orderby h.IdCuentaContableNavigation.Numero
                select h;
            return JerarquiaEstructura.FirstOrDefault();
        }

        public JerarquiaEstructura GetByDescription(int hierarchyId, string description, string number)
        {
            var JerarquiaEstructura =
                from h in context.JerarquiaEstructura
                where h.IdJerarquia == hierarchyId
                && h.IdAuxiliar == null
                && h.IdCuentaContable == null
                && h.Descripcion == description
                && h.Numero == number
                orderby h.IdCuentaContableNavigation.Numero
                select h;
            return JerarquiaEstructura.FirstOrDefault();
        }

        public JerarquiaEstructura Consultar(int hierarchyStructureId)
        {
            var JerarquiaEstructura =
                from hs in context.JerarquiaEstructura
                    .Include(je => je.IdCuentaContableNavigation)
                where hs.IdJerarquiaEstructura == hierarchyStructureId
                select hs;
            return JerarquiaEstructura.FirstOrDefault();
        }

        public IEnumerable<JerarquiaEstructura> ConsultarPorJerarquia(int idJerarquia)
        {
            var JerarquiaEstructura =
                from hs in context.JerarquiaEstructura
                where hs.IdJerarquia == idJerarquia
                select hs;
            return JerarquiaEstructura.ToList();
        }

        public List<JerarquiaEstructuraDto> ObtenerSaldo(int hierarchyId, int year, int month, int? initialYear, int? initialMonth,
            int? hierarchyStructureAuxiliaryId, int? hierarchyIdTypeAuxiliary, bool budgetary, int? versionId)
        {
            var jerarquias = new List<JerarquiaEstructuraDto>();

            using (var cmd = context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "[dbo].[sp_BalanceCuentaContable]";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("IdJerarquia", hierarchyId));
                cmd.Parameters.Add(new SqlParameter("Anio", year));
                cmd.Parameters.Add(new SqlParameter("Mes", month));
                cmd.Parameters.Add(new SqlParameter("AnioInicial", initialYear));
                cmd.Parameters.Add(new SqlParameter("MesInicial", initialMonth));
                cmd.Parameters.Add(new SqlParameter("IdJerarquiaEstructuraAuxiliar", hierarchyStructureAuxiliaryId));
                cmd.Parameters.Add(new SqlParameter("IdJerarquiaTipoAuxiliar", hierarchyIdTypeAuxiliary));
                cmd.Parameters.Add(new SqlParameter("EsPresupuesto", budgetary));
                cmd.Parameters.Add(new SqlParameter("IdVersionPoliza", versionId));

                using (var reader = cmd.ExecuteReader())
                {
                    jerarquias = reader.Select(r => new JerarquiaEstructuraDto
                    {
                        IdJerarquiaEstructura = int.Parse(r["IdJerarquiaEstructura"].ToString()),
                        IdJerarquiaEstructuraPadre = r["IdJerarquiaEstructuraPadre"] is DBNull ? null : int.Parse(r["IdJerarquiaEstructuraPadre"].ToString()),
                        IdCuentaContable = r["IdCuentaContable"] is DBNull ? null : int.Parse(r["IdCuentaContable"].ToString()),
                        IdAuxiliar = r["IdAuxiliar"] is DBNull ? null : int.Parse(r["IdAuxiliar"].ToString()),
                        Descripcion = r["Descripcion"] is DBNull ? null : r["Descripcion"].ToString(),
                        SaldoInicial = decimal.Parse(r["SaldoInicial"].ToString()),
                        Cargo = r["Cargo"] is DBNull ? 0 : decimal.Parse(r["Cargo"].ToString()),
                        Abono = r["Abono"] is DBNull ? 0 : decimal.Parse(r["Abono"].ToString()),
                        SaldoFinal = decimal.Parse(r["Saldo"].ToString()),
                        ClaveSaldoInicial = r["ClaveSaldoInicial"] is DBNull ? null : r["ClaveSaldoInicial"].ToString(),
                        ClaveCargo = r["ClaveCargo"] is DBNull ? null : r["ClaveCargo"].ToString(),
                        ClaveAbono = r["ClaveAbono"] is DBNull ? null : r["ClaveAbono"].ToString(),
                        ClaveSaldoFinal = r["ClaveSaldoFinal"] is DBNull ? null : r["ClaveSaldoFinal"].ToString(),
                        TotalHijos = r["TotalHijos"] is DBNull ? 0 : int.Parse(r["TotalHijos"].ToString()),
                        TieneMovimientos = int.Parse(r["TieneMovimientos"].ToString()) > 0
                    }).ToList();
                }
            }

            return jerarquias;
        }

        public List<JerarquiaEstructuraDto> GetConfiguration(int hierarchyId)
        {
            var jerarquias = new List<JerarquiaEstructuraDto>();

            using (var cmd = context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "[dbo].[sp_ConfiguracionEstructura]";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("IdJerarquia", hierarchyId));

                using (var reader = cmd.ExecuteReader())
                {
                    jerarquias = reader.Select(r => new JerarquiaEstructuraDto
                    {
                        IdJerarquiaEstructura = int.Parse(r["IdJerarquiaEstructura"].ToString()),
                        IdJerarquiaEstructuraPadre = r["IdJerarquiaEstructuraPadre"] is DBNull ? null : int.Parse(r["IdJerarquiaEstructuraPadre"].ToString()),
                        Descripcion = r["Descripcion"] is DBNull ? null : r["Descripcion"].ToString(),
                        TotalHijos = int.Parse(r["TotalHijos"].ToString()),
                        IdCuentaContable = r["IdCuentaContable"] is DBNull ? null : int.Parse(r["IdCuentaContable"].ToString()),
                        IdAuxiliar = r["IdAuxiliar"] is DBNull ? null : int.Parse(r["IdAuxiliar"].ToString()),
                    }).ToList();
                }
            }

            return jerarquias;
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaArbol(int idJerarquia)
        {
            return context.JerarquiaEstructura
                .Where(je => je.IdJerarquia == idJerarquia)
                .Select(je => new JerarquiaEstructuraArbolDto()
                {
                    IdJerarquiaEstructura = je.IdJerarquiaEstructura,
                    IdJerarquiaEstructuraPadre = je.IdJerarquiaEstructuraPadre,
                    Cuenta = je.IdCuentaContableNavigation != null
                        ? je.IdCuentaContableNavigation.Numero + " - " + je.IdCuentaContableNavigation.Descripcion
                        : je.IdAuxiliar != null
                            ? je.IdAuxiliarNavigation.Numero + " - " + je.IdAuxiliarNavigation.Descripcion
                            : je.Numero + " - " + je.Descripcion
                });
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPadres(int idJerarquia)
        {
            return ConsultarPorJerarquiaArbol(idJerarquia).Where(je => je.IdJerarquiaEstructuraPadre == null);
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarHijos(int idJerarquiaEstructuraPadre)
        {
            return context.JerarquiaEstructura
                .Where(je => je.IdJerarquiaEstructuraPadre == idJerarquiaEstructuraPadre)
                .Select(je => new JerarquiaEstructuraArbolDto()
                {
                    IdJerarquiaEstructura = je.IdJerarquiaEstructura,
                    IdJerarquiaEstructuraPadre = je.IdJerarquiaEstructuraPadre,
                    Cuenta = je.IdCuentaContableNavigation != null
                        ? je.IdCuentaContableNavigation.Numero + " - " + je.IdCuentaContableNavigation.Descripcion
                        : je.IdAuxiliar != null
                            ? je.IdAuxiliarNavigation.Numero + " - " + je.IdAuxiliarNavigation.Descripcion
                            : je.Numero + " - " + je.Descripcion
                });
        }

        public List<JerarquiaEstructura> ConsultarHijosDeEstructura(int idJerarquiaEstructuraPadre)
        {
            return context.JerarquiaEstructura
                .Where(je => je.IdJerarquiaEstructuraPadre == idJerarquiaEstructuraPadre)
                .ToList();
        }

    }
}
