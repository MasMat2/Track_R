using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class JerarquiaAccesoEstructuraRepository : Repository<JerarquiaAccesoEstructura>, IJerarquiaAccesoEstructuraRepository
    {
        public JerarquiaAccesoEstructuraRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public JerarquiaAccesoEstructura Consultar(int idJerarquiaAccesoEstructura)
        {
            return context.JerarquiaAccesoEstructura
                .Include(je => je.IdAccesoNavigation)
                .Where(je => je.IdJerarquiaAccesoEstructura == idJerarquiaAccesoEstructura)
                .FirstOrDefault();
        }

        public IEnumerable<JerarquiaAccesoEstructura> ConsultarPorJerarquiaAcceso(int idJerarquiaAcceso)
        {
            return context.JerarquiaAccesoEstructura
                .Where(je => je.IdJerarquiaAcceso == idJerarquiaAcceso)
                .ToList();
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaArbol(int idJerarquia)
        {
            return context.JerarquiaAccesoEstructura
                .Where(je => je.IdJerarquiaAcceso == idJerarquia)
                .Select(je => new JerarquiaEstructuraArbolDto()
                {
                    IdJerarquiaEstructura = je.IdJerarquiaAccesoEstructura,
                    IdJerarquiaEstructuraPadre = je.IdJerarquiaAccesoEstructuraPadre,
                    Cuenta = je.IdAccesoNavigation.Nombre,
                    IdAcceso = je.IdAcceso,
                    TipoAcceso = je.IdAccesoNavigation.IdTipoAccesoNavigation.Nombre
                })
                .OrderBy(je => je.IdJerarquiaEstructura)
                .ToList();
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaParaSelector(int idJerarquia)
        {
            return context.JerarquiaAccesoEstructura
                .Include(je => je.IdAccesoNavigation.IdTipoAccesoNavigation)
                .Where(je => je.IdJerarquiaAcceso == idJerarquia &&
                             je.IdAccesoNavigation.IdTipoAccesoNavigation.Clave != GeneralConstant.ClaveTipoAccesoEvento)
                .Select(je => new JerarquiaEstructuraArbolDto()
                {
                    IdJerarquiaEstructura = je.IdJerarquiaAccesoEstructura,
                    IdJerarquiaEstructuraPadre = je.IdJerarquiaAccesoEstructuraPadre,
                    Cuenta = je.IdAccesoNavigation.Clave + " - " + je.IdAccesoNavigation.Nombre,
                    IdAcceso = je.IdAcceso
                });
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPadres(int idJerarquia)
        {
            return ConsultarPorJerarquiaArbol(idJerarquia).Where(je => je.IdJerarquiaEstructuraPadre == null);
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarHijos(int idJerarquiaEstructuraPadre)
        {
            return context.JerarquiaAccesoEstructura
                .Include(je => je.IdAccesoNavigation.IdTipoAccesoNavigation)
                .Where(je => je.IdJerarquiaAccesoEstructuraPadre == idJerarquiaEstructuraPadre)
                .Select(je => new JerarquiaEstructuraArbolDto()
                {
                    IdJerarquiaEstructura = je.IdJerarquiaAccesoEstructura,
                    IdJerarquiaEstructuraPadre = je.IdJerarquiaAccesoEstructuraPadre,
                    Cuenta = je.IdAccesoNavigation.Nombre,
                    IdAcceso = je.IdAcceso,
                    TipoAcceso = je.IdAccesoNavigation.IdTipoAccesoNavigation.Nombre
                });
        }

        public List<JerarquiaAccesoEstructura> ConsultarHijosDeEstructura(int idJerarquiaAccesoEstructuraPadre)
        {
            return context.JerarquiaAccesoEstructura
                .Where(je => je.IdJerarquiaAccesoEstructuraPadre == idJerarquiaAccesoEstructuraPadre)
                .ToList();
        }
    }
}
