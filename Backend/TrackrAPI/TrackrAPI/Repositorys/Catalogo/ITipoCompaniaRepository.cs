using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ITipoCompaniaRepository : IRepository<TipoCompania>
    {
        TipoCompania Consultar(int idTipoCompania);
        TipoCompania ConsultarPorClave(string clave);
        TipoCompania ConsultarPorNombre(string nombre);
        IEnumerable<TipoCompaniaSelectorDto> ConsultarParaSelector();
        IEnumerable<TipoCompania> ConsultarTodosParaGrid();
        TipoCompania ConsultarDependencias(int idTipoCompania);

    }
}
