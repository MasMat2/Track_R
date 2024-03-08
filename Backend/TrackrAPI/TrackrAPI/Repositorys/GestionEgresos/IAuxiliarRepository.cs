using TrackrAPI.Dtos.GestionEgresos;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionEgresos
{
    public interface IAuxiliarRepository : IRepository<Auxiliar>
    {
        public IEnumerable<AuxiliarSelectorDto> consultarPorTipoAuxiliarParaSelector(int idTipoAuxiliar, int idCompania);
        public IEnumerable<AuxiliarSelectorDto> ConsultarPorTipoBalanceParaSelector(int idCompania);
        public IEnumerable<AuxiliarSelectorDto> ConsultarParaSelectorVehiculo(int idCompania);
        public IEnumerable<AuxiliarSelectorDto> ConsultarParaSelector(int idCompania);
        public IEnumerable<AuxiliarGridDto> consultarTodosParaGrid(int idCompania);
        public IEnumerable<AuxiliarGridDto> ConsultarParaJerarquiaGrid(int idJerarquia, string clave);
        public Auxiliar Consultar(int idAuxiliar);
        public Auxiliar ConsultarPorNumero(string numero, int idCompania);
        public Auxiliar ConsultarPorNumero(string numero, int idTipoAuxiliar, int idCompania);
        public Auxiliar ConsultarPorDescripcion(string descripcion, int idCompania);
        public Auxiliar ConsultarPorJerarquia(int auxiliaryId);
        public Auxiliar ConsultarDependencias(int idAuxiliar);
    }
}
