using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public interface IAgrupadorCuentaContableRepository : IRepository<AgrupadorCuentaContable>
    {
        public AgrupadorCuentaContable ConsultarPorCompania(int idCompania);
        public AgrupadorCuentaContable Consultar(int idAgrupadorCuentaContable);
        public AgrupadorCuentaContable ConsultarPorClave(string clave);
        public AgrupadorCuentaContableDto ConsultarDto(int idAgrupadorCuentaContable);
        public IEnumerable<AgrupadorCuentaContableDto> ConsultarParaGrid();
        public IEnumerable<AgrupadorCuentaContableDto> ConsultarParaSelector();
        public AgrupadorCuentaContable ConsultarDependencias(int idAgrupadorCuentaContable);
    }
}
