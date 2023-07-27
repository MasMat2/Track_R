using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ITipoFlujoRepository : IRepository<TipoFlujo>
    {
        IEnumerable<TipoFlujo> ConsultarTodos();
        TipoFlujo Consultar(int idTipoFlujo);
        TipoFlujo ConsultarPorClave(string clave);
        TipoFlujo ConsultarPorNombre(string nombre);
        TipoFlujo ConsultarDependencias(int idTipoFlujo);
    }
}
