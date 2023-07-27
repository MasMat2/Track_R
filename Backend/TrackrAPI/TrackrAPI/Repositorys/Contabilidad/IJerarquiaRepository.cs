using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public interface IJerarquiaRepository : IRepository<Jerarquia>
    {
        public IEnumerable<Jerarquia> GetByAccountGroupingDefault(int accountGroupingId);
        public IEnumerable<JerarquiaSelectorDto> ConsultarParaSelector(int idCompania, bool obtenerTipoCuenta);
        public IEnumerable<JerarquiaSelectorDto> ConsultarParaSelector(int idCompania);
        public IEnumerable<JerarquiaGridDto> ConsultarParaGrid(int idCompania, string claveTipoAxuiliar);
        public IEnumerable<JerarquiaGridDto> ConsultarParaGrid(int idCompania);
        public Jerarquia Consultar(int idJerarquia);
        public Jerarquia ConsultarEstandar(int idCompania, string claveTipoAuxiliar);
    }
}
