using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public interface IJerarquiaEstructuraRepository : IRepository<JerarquiaEstructura>
    {
        public List<JerarquiaEstructuraDto> ObtenerSaldo(int hierarchyId, int year, int month, int? initialYear, int? initialMonth,
            int? hierarchyStructureAuxiliaryId, int? hierarchyIdTypeAuxiliary, bool budgetary, int? versionId);
        public JerarquiaEstructura Consultar(int idJerarquiaEstructura);
        public JerarquiaEstructura ConsultarPorCuentaContable(int idJerarquia, int idCuentaContable);
        public JerarquiaEstructura ConsultarPorAuxiliar(int idJerarquia, int idAuxiliar);
        public JerarquiaEstructura GetByDescription(int hierarchyId, string description, string number);
        public IEnumerable<JerarquiaEstructura> ConsultarPorJerarquia(int idJerarquia);
        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaArbol(int idJerarquia);
        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPadres(int idJerarquia);
        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarHijos(int idJerarquiaEstructuraPadre);
        public List<JerarquiaEstructuraDto> GetConfiguration(int hierarchyId);
        public List<JerarquiaEstructura> ConsultarHijosDeEstructura(int idJerarquiaEstructura);
    }
}
