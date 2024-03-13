using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public class JerarquiaRepository : Repository<Jerarquia>, IJerarquiaRepository
    {
        public JerarquiaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<Jerarquia> GetByAccountGroupingDefault(int accountGroupingId)
        {
            int? idCompaniaBase = context.Compania.FirstOrDefault(cc => cc.Clave == GeneralConstant.ClaveCompaniaBase)?.IdCompania;

            return context.Jerarquia
                .Where(h => h.IdCompania == idCompaniaBase
                    && h.IdTipoAuxiliarNavigation.Clave == GeneralConstant.TypeAuxiliaryCodeAccount
                    && h.JerarquiaEstructura.FirstOrDefault().IdCuentaContableNavigation.IdAgrupadorCuentaContable == accountGroupingId)
                .ToList();
        }

        public IEnumerable<JerarquiaSelectorDto> ConsultarParaSelector(int idCompania)
        {
            return context.Jerarquia
                .Where(j => j.IdCompania == idCompania)
                .Select(j => new JerarquiaSelectorDto
                {
                    IdJerarquia = j.IdJerarquia,
                    ClaveTipoAuxiliar = j.IdTipoAuxiliarNavigation.Clave,
                    Nombre = j.Nombre,
                    Estandar = j.Estandar
                })
                .ToList();
        }

        public IEnumerable<JerarquiaSelectorDto> ConsultarParaSelector(int idCompania, bool obtenerTipoCuenta)
        {
            return context.Jerarquia
                .Where(j => j.IdCompania == idCompania
                 && ((j.IdTipoAuxiliarNavigation.Clave == GeneralConstant.TypeAuxiliaryCodeAccount && obtenerTipoCuenta)
                || (j.IdTipoAuxiliarNavigation.Clave != GeneralConstant.TypeAuxiliaryCodeAccount && !obtenerTipoCuenta)))
                .Select(j => new JerarquiaSelectorDto
                {
                    IdJerarquia = j.IdJerarquia,
                    Nombre = j.Nombre,
                    Estandar = j.Estandar,
                    NombreTipoAuxiliar = j.IdTipoAuxiliarNavigation.Descripcion
                })
                .ToList();
        }

        public IEnumerable<JerarquiaGridDto> ConsultarParaGrid(int idCompania, string claveTipoAxuiliar)
        {
            return context.Jerarquia
                .Where(j => j.IdTipoAuxiliarNavigation.Clave == claveTipoAxuiliar && (j.IdCompania == idCompania))
                .Select(j => new JerarquiaGridDto
                {
                    IdJerarquia = j.IdJerarquia,
                    IdCompania = j.IdCompania,
                    Nombre = j.Nombre,
                    InvertirSigno = j.InvertirSigno,
                    Estandar = j.Estandar
                });
        }

        public IEnumerable<JerarquiaGridDto> ConsultarParaGrid(int idCompania)
        {
            return context.Jerarquia
                .Where(j => (j.IdCompania == idCompania))
                .Select(j => new JerarquiaGridDto
                {
                    IdJerarquia = j.IdJerarquia,
                    IdCompania = j.IdCompania,
                    Nombre = j.Nombre,
                    InvertirSigno = j.InvertirSigno,
                    Estandar = j.Estandar
                });
        }

        public Jerarquia Consultar(int idJerarquia)
        {
            return context.Jerarquia
                .Include(j => j.IdCompaniaNavigation.IdMonedaNavigation)
                .Where(j => j.IdJerarquia == idJerarquia)
                .FirstOrDefault();
        }

        public Jerarquia ConsultarEstandar(int idCompania, string claveTipoAuxiliar)
        {
            return context.Jerarquia
                .Where(j => j.IdCompania == idCompania && j.Estandar == true && j.IdTipoAuxiliarNavigation.Clave == claveTipoAuxiliar)
                .FirstOrDefault();
        }

    }
}
