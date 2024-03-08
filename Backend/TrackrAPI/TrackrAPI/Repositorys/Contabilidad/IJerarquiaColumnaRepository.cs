using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public interface IJerarquiaColumnaRepository : IRepository<JerarquiaColumna>
    {
        public JerarquiaColumnaDto ConsultarDto(int idJerarquiaColumna);
        public JerarquiaColumna Consultar(int idJerarquiaColumna);
        public JerarquiaColumna ConsultarPorNombre(string nombre, int idJerarquia);
        public IEnumerable<JerarquiaColumna> ConsultarPorJerarquia(int hierarchyId);
        public IEnumerable<JerarquiaColumnaDto> ConsultarPorJerarquiaDto(int hierarchyId);
        public IEnumerable<JerarquiaColumnaDto> ConsultarPorJerarquiaNoUsada(int hierarchyStructureId);
    }
}
