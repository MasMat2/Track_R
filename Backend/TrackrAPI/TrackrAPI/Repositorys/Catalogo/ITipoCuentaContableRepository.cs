using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ITipoCuentaContableRepository : IRepository<TipoCuentaContable>
    {
        public IEnumerable<TipoCuentaContableDto> ConsultarTodosParaGrid();
        public IEnumerable<TipoCuentaContableDto> ConsultarTodosParaSelector();
        public TipoCuentaContable Consultar(int idTipoCuentaContable);
        public TipoCuentaContableDto ConsultarDto(int idTipoCuentaContable);
        public TipoCuentaContable ConsultarPorNombre(string nombre);
        public TipoCuentaContable ConsultarPorClave(string clave);
        public TipoCuentaContable ConsultarPorCuentaContable(int idCuentaContable);
        public TipoCuentaContable ConsultarDependencias(int idTipoCuentaContable);

    }
}
