using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class JerarquiaAccesoRepository : Repository<JerarquiaAcceso>, IJerarquiaAccesoRepository
    {
        public JerarquiaAccesoRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public JerarquiaAcceso Consultar(int idJerarquiaAcceso)
        {
            return context.JerarquiaAcceso
                    .Include(j => j.JerarquiaAccesoEstructura)
                    .Include(j => j.JerarquiaAccesoTipoCompania)
                    .Where(j => j.IdJerarquiaAcceso == idJerarquiaAcceso)
                    .FirstOrDefault();
        }

        public JerarquiaAccesoDto ConsultarDto(int idJerarquiaAcceso)
        {
            return context.JerarquiaAcceso
                    .Include(j => j.JerarquiaAccesoEstructura)
                    .Include(j => j.JerarquiaAccesoTipoCompania)
                    .Where(j => j.IdJerarquiaAcceso == idJerarquiaAcceso)
                    .Select(j => new JerarquiaAccesoDto
                    {
                        IdJerarquiaAcceso = j.IdJerarquiaAcceso,
                        IdCompania = j.IdCompania,
                        Nombre = j.Nombre,
                        IdsTipoCompania = j.JerarquiaAccesoTipoCompania.Select(jat => jat.IdTipoCompania).ToList()
                    })
                    .FirstOrDefault();
        }

        public JerarquiaAcceso ConsultarDependencias(int idJerarquiaAcceso)
        {
            return context.JerarquiaAcceso
                .Include(j => j.JerarquiaAccesoEstructura)
                .Include(j => j.JerarquiaAccesoTipoCompania)
                .Include(j => j.Perfil)
                .Where(j => j.IdJerarquiaAcceso == idJerarquiaAcceso)
                .FirstOrDefault();
        }

        public IEnumerable<JerarquiaAccesoDto> ConsultarParaGrid(int idCompania)
        {
            return context.JerarquiaAcceso
                    .Include(j => j.JerarquiaAccesoEstructura)
                    .Where(j => j.IdCompania == idCompania)
                    .Select(j => new JerarquiaAccesoDto {
                        IdJerarquiaAcceso = j.IdJerarquiaAcceso,
                        IdCompania = j.IdCompania,
                        Nombre = j.Nombre,
                        NombreTipoCompania = string.Join(", ",j.JerarquiaAccesoTipoCompania.Select(jat => jat.IdTipoCompaniaNavigation.Nombre))
                    })
                    .ToList();
        }

        public IEnumerable<JerarquiaAccesoDto> ConsultarParaSelector(string claveTipoCompania)
        {
            return context.JerarquiaAcceso
                    .Include(j => j.JerarquiaAccesoEstructura)
                    .Include(j => j.JerarquiaAccesoTipoCompania)
                        .ThenInclude(jat => jat.IdTipoCompaniaNavigation)
                    .Where(j => j.JerarquiaAccesoTipoCompania
                        .Any(c => string.IsNullOrEmpty(claveTipoCompania) || c.IdTipoCompaniaNavigation.Clave == claveTipoCompania))
                    .Select(j => new JerarquiaAccesoDto
                    {
                        IdJerarquiaAcceso = j.IdJerarquiaAcceso,
                        IdCompania = j.IdCompania,
                        Nombre = j.Nombre,
                    })
                    .ToList();
        }
    }
}
