using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IJerarquiaAccesoEstructuraRepository : IRepository<JerarquiaAccesoEstructura>
    {
        JerarquiaAccesoEstructura Consultar(int idJerarquiaAccesoEstructura);
        IEnumerable<JerarquiaAccesoEstructura> ConsultarPorJerarquiaAcceso(int idJerarquiaAcceso);
        IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaArbol(int idJerarquia);
        IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaParaSelector(int idJerarquia);
        IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPadres(int idJerarquia);
        IEnumerable<JerarquiaEstructuraArbolDto> ConsultarHijos(int idJerarquiaEstructuraPadre);
        List<JerarquiaAccesoEstructura> ConsultarHijosDeEstructura(int idJerarquiaAccesoEstructuraPadre);
    }
}
