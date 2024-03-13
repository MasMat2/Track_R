using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ICuentaContableRepository : IRepository<CuentaContable>
    {
        public IEnumerable<CuentaContableDto> ConsultarTodosParaSelector(int idCompania);
        public IEnumerable<CuentaContableDto> ConsultarPorAgrupadorParaSelector(int idAgrupador);
        public IEnumerable<CuentaContable> ConsultarPorCompaniaBaseYAgrupador(int idAgrupador);
        public IEnumerable<CuentaContableGridDto> ConsultarPorAgrupadorParaGrid(int idAgrupador);
        public CuentaContable Consultar(int idCuentaContable);
        public CuentaContableDto ConsultarDto(int idCuentaContable);
        public IEnumerable<CuentaContableGridDto> ConsultarTodosParaGrid(int idCompania);
        public IEnumerable<CuentaContableGridDto> ConsultarPorFiltroParaGrid(int idCompania, CuentaContableFiltroDto filtro);
        public CuentaContable ConsultarPorNumero(int idCompania, string numero);
        public CuentaContable ConsultarPorNombre(int idCompania, string nombre);
        public CuentaContable ConsultarDependencias(int idCuentaContable);
        public IEnumerable<CuentaContable> ConsultarParaJerarquiaGrid(int idJerarquia);
        public IEnumerable<CuentaPartidaVivaGridDto> ConsultarConPartidasAbiertas(int idCompania);
        public CuentaContable ConsultarPorJerarquia(int idCuentaContable, int idCompania);
    }
}
