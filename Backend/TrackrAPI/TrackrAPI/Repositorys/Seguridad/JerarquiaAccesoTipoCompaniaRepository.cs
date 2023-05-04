using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class JerarquiaAccesoTipoCompaniaRepository : Repository<JerarquiaAccesoTipoCompania>, IJerarquiaAccesoTipoCompaniaRepository
    {
        public JerarquiaAccesoTipoCompaniaRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public JerarquiaAccesoTipoCompania Consultar(int idJerarquiaAccesoTipoCompania)
        {
            return context.JerarquiaAccesoTipoCompania
                .Where(jat => jat.IdJerarquiaAccesoTipoCompania == idJerarquiaAccesoTipoCompania)
                .FirstOrDefault();
        }

        public IEnumerable<JerarquiaAccesoTipoCompania> ConsultarPorJerarquiaAcceso(int idJerarquiaAcceso)
        {
            return context.JerarquiaAccesoTipoCompania
                .Where(jat => jat.IdJerarquiaAcceso == idJerarquiaAcceso)
                .ToList();
        }
    }
}
