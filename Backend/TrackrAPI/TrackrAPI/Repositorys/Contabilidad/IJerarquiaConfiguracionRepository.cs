using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public interface IJerarquiaConfiguracionRepository : IRepository<JerarquiaConfiguracion>
    {
        JerarquiaConfiguracion GetByCode(int hierarchyId, string code);
        JerarquiaConfiguracion GetByHierarchyStructure(int hierarchyStructureId, int hierarchyColumnId);
        JerarquiaConfiguracion Consultar(int idJerarquiaConfiguracion);
        JerarquiaConfiguracionDto ConsultarDto(int idJerarquiaConfiguracion);
        List<JerarquiaConfiguracion> GetByHierarchyStructure(int hierarchyStructureId);
        IEnumerable<JerarquiaConfiguracionDto> ConsultarPorJerarquiaEstructuraDto(int hierarchyStructureId);
        IEnumerable<JerarquiaConfiguracion> ConsultarPorJerarquia(int idJerarquia);

    }
}
