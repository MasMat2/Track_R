using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IJerarquiaAccesoRepository : IRepository<JerarquiaAcceso>
    {
        public JerarquiaAcceso Consultar(int idJerarquiaAcceso);
        public JerarquiaAccesoDto ConsultarDto(int idJerarquiaAcceso);
        public JerarquiaAcceso ConsultarDependencias(int idJerarquiaAcceso);
        public IEnumerable<JerarquiaAccesoDto> ConsultarParaGrid(int idCompania);
        public IEnumerable<JerarquiaAccesoDto> ConsultarParaSelector(string claveTipoCompania);
    }
}
