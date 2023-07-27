using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IJerarquiaAccesoTipoCompaniaRepository : IRepository<JerarquiaAccesoTipoCompania>
    {
        JerarquiaAccesoTipoCompania Consultar(int idJerarquiaAccesoTipoCompania);
        IEnumerable<JerarquiaAccesoTipoCompania> ConsultarPorJerarquiaAcceso(int idJerarquiaAcceso);
    }
}
