using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ISubtipoCuentaContableRepository
    {
        public IEnumerable<SubtipoCuentaContableDto> ConsultarTodosParaSelector();
        public IEnumerable<SubtipoCuentaContableDto> ConsultarPorTipoParaSelector(int idTipoCuentaContable);
        public SubtipoCuentaContableDto ConsultarDto(int idTipoCuentaContable);
        public SubtipoCuentaContable Consultar(int idTipoCuentaContable);
    }
}
