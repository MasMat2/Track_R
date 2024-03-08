using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public class JerarquiaConfiguracionRepository : Repository<JerarquiaConfiguracion>, IJerarquiaConfiguracionRepository
    {
        public JerarquiaConfiguracionRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public JerarquiaConfiguracion Consultar(int hierarchyConfigurationId)
        {
            var JerarquiaConfiguracion =
                from p in context.JerarquiaConfiguracion
                .Include(p => p.IdJerarquiaColumnaNavigation)
                .Include(p => p.IdJerarquiaEstructuraNavigation)
                .Include(p => p.IdJerarquiaConfiguracionRelacionadoNavigation.IdJerarquiaEstructuraNavigation)
                where p.IdJerarquiaConfiguracion == hierarchyConfigurationId
                select p;
            return JerarquiaConfiguracion.FirstOrDefault();
        }

        public JerarquiaConfiguracionDto ConsultarDto(int hierarchyConfigurationId)
        {
            var JerarquiaConfiguracion =
                from p in context.JerarquiaConfiguracion
                .Include(p => p.IdJerarquiaColumnaNavigation)
                .Include(p => p.IdJerarquiaEstructuraNavigation)
                .Include(p => p.IdJerarquiaConfiguracionRelacionadoNavigation.IdJerarquiaEstructuraNavigation)
                where p.IdJerarquiaConfiguracion == hierarchyConfigurationId
                select new JerarquiaConfiguracionDto
                {
                    IdJerarquiaConfiguracion = p.IdJerarquiaConfiguracion,
                    Clave = p.Clave,
                    Formula = p.Formula,
                    IdJerarquiaColumna = p.IdJerarquiaColumna,
                    IdJerarquiaConfiguracionRelacionado = p.IdJerarquiaConfiguracionRelacionado,
                    IdJerarquiaEstructura = p.IdJerarquiaEstructura,
                    AgregadoPorSistema = p.AgregadoPorSistema,
                    IdJerarquiaEstructuraRelacionado = p.IdJerarquiaConfiguracionRelacionadoNavigation.IdJerarquiaEstructura,
                    IdJerarquiaRelacionada = p.IdJerarquiaConfiguracionRelacionadoNavigation.IdJerarquiaEstructuraNavigation.IdJerarquia
                };
            return JerarquiaConfiguracion.FirstOrDefault();
        }

        public JerarquiaConfiguracion GetByCode(int hierarchyId, string code)
        {
            var hierarchyConfiguration =
                from hc in context.JerarquiaConfiguracion
                    .Include(p => p.IdJerarquiaColumnaNavigation)
                    .Include(p => p.IdJerarquiaEstructuraNavigation)
                    .Include(p => p.IdJerarquiaConfiguracionRelacionadoNavigation)
                where hc.IdJerarquiaEstructuraNavigation.IdJerarquia == hierarchyId
                && hc.Clave == code
                select hc;
            return hierarchyConfiguration.FirstOrDefault();
        }

        public JerarquiaConfiguracion GetByHierarchyStructure(int hierarchyStructureId, int hierarchyColumnId)
        {
                return context.JerarquiaConfiguracion
                    .Include(p => p.IdJerarquiaColumnaNavigation)
                    .Include(p => p.IdJerarquiaEstructuraNavigation)
                    .Include(p => p.IdJerarquiaConfiguracionRelacionadoNavigation)
                .Where(h =>
                    h.IdJerarquiaEstructuraNavigation.IdJerarquiaEstructura == hierarchyStructureId
                    && h.IdJerarquiaColumna == hierarchyColumnId)
                .FirstOrDefault();
        }

        public List<JerarquiaConfiguracion> GetByHierarchyStructure(int hierarchyStructureId)
        {
            var hierarchyConfiguration =
                from h in context.JerarquiaConfiguracion
                where h.IdJerarquiaEstructuraNavigation.IdJerarquiaEstructura == hierarchyStructureId
                select h;
            return hierarchyConfiguration.ToList();
        }

        public IEnumerable<JerarquiaConfiguracion> ConsultarPorJerarquia(int idJerarquia)
        {
            var hierarchyConfiguration =
                from h in context.JerarquiaConfiguracion
                    .Include(p => p.IdJerarquiaColumnaNavigation)
                    .Include(p => p.IdJerarquiaEstructuraNavigation)
                    .Include(p => p.IdJerarquiaConfiguracionRelacionadoNavigation)
                where h.IdJerarquiaEstructuraNavigation.IdJerarquia == idJerarquia
                select h;
            return hierarchyConfiguration.ToList();
        }

        public IEnumerable<JerarquiaConfiguracionDto> ConsultarPorJerarquiaEstructuraDto(int hierarchyStructureId)
        {
            var hierarchyConfiguration =
                from h in context.JerarquiaConfiguracion
                where h.IdJerarquiaEstructuraNavigation.IdJerarquiaEstructura == hierarchyStructureId
                select new JerarquiaConfiguracionDto
                {
                    IdJerarquiaConfiguracion = h.IdJerarquiaConfiguracion,
                    AgregadoPorSistema = h.AgregadoPorSistema,
                    Clave = h.Clave,
                    Formula = h.Formula,
                    IdJerarquiaColumna = h.IdJerarquiaColumna,
                    IdJerarquiaConfiguracionRelacionado = h.IdJerarquiaConfiguracionRelacionado,
                    IdJerarquiaEstructura = h.IdJerarquiaEstructura,
                    NombreColumna = h.IdJerarquiaColumnaNavigation.Nombre,
                    CuentaRelacionada = h.IdJerarquiaConfiguracionRelacionadoNavigation.IdJerarquiaEstructuraNavigation.IdCuentaContableNavigation.Numero
                        + " - " + h.IdJerarquiaConfiguracionRelacionadoNavigation.IdJerarquiaEstructuraNavigation.IdCuentaContableNavigation.Nombre,
                    ColumnaRelacionada = h.IdJerarquiaConfiguracionRelacionadoNavigation.IdJerarquiaColumnaNavigation.Nombre
                };
            return hierarchyConfiguration.ToList();
        }

    }
}
