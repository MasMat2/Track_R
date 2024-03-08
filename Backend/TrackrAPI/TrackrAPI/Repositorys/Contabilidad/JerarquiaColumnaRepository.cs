using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public class JerarquiaColumnaRepository : Repository<JerarquiaColumna>, IJerarquiaColumnaRepository
    {
        public JerarquiaColumnaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public JerarquiaColumna Consultar(int idJerarquiaColumna)
        {
            return
                context.JerarquiaColumna
                .Where(hc => hc.IdJerarquiaColumna == idJerarquiaColumna)
                .FirstOrDefault();
        }

        public JerarquiaColumnaDto ConsultarDto(int idJerarquiaColumna)
        {
            return
                context.JerarquiaColumna
                .Where(hc => hc.IdJerarquiaColumna == idJerarquiaColumna)
                .Select(hc => new JerarquiaColumnaDto
                {
                    IdJerarquiaColumna = hc.IdJerarquiaColumna,
                    Nombre = hc.Nombre,
                    IdJerarquia = hc.IdJerarquia,
                    Acumula = hc.Acumula,
                    AgregadoPorSistema = hc.AgregadoPorSistema,
                    Anio = hc.Anio,
                    EsPorcentaje = hc.EsPorcentaje,
                    IdJerarquiaColumnaRelacionada = hc.IdJerarquiaColumnaRelacionada,
                    IdVersionPoliza = hc.IdVersionPoliza,
                    Letra = hc.Letra,
                    Mes = hc.Mes
                })
                .FirstOrDefault();
        }

        public JerarquiaColumna ConsultarPorNombre(string nombre, int idJerarquia)
        {
            return
                context.JerarquiaColumna
                .Where(hc => hc.Nombre == nombre && hc.IdJerarquia == idJerarquia)
                .FirstOrDefault();
        }

        public IEnumerable<JerarquiaColumna> ConsultarPorJerarquia(int idJerarquia)
        {
            return
                context.JerarquiaColumna
                .Where(hc => hc.IdJerarquia == idJerarquia)
                .ToList();
        }

        public IEnumerable<JerarquiaColumnaDto> ConsultarPorJerarquiaDto(int idJerarquia)
        {
            return
                context.JerarquiaColumna
                .Where(hc => hc.IdJerarquia == idJerarquia)
                .Select(hc => new JerarquiaColumnaDto
                {
                    IdJerarquiaColumna = hc.IdJerarquiaColumna,
                    Nombre = hc.Nombre
                })
                .ToList();
        }

        public IEnumerable<JerarquiaColumnaDto> ConsultarPorJerarquiaNoUsada(int hierarchyStructureId)
        {

            int hierarchyId =
                (from h in context.JerarquiaEstructura
                 where h.IdJerarquiaEstructura == hierarchyStructureId
                 select h.IdJerarquia).FirstOrDefault();

            var hierarchyColumnUsedList =
                (from h in context.JerarquiaConfiguracion
                 where h.IdJerarquiaEstructura == hierarchyStructureId
                 select h.IdJerarquiaColumna);

            var hierarchyColumnList =
                from hc in context.JerarquiaColumna
                where hc.IdJerarquia == hierarchyId
                && !hc.AgregadoPorSistema
                && !hierarchyColumnUsedList.Contains(hc.IdJerarquiaColumna)
                select new JerarquiaColumnaDto
                {
                    IdJerarquiaColumna = hc.IdJerarquiaColumna,
                    Nombre = hc.Nombre
                };

            return hierarchyColumnList.ToList();
        }

    }
}
