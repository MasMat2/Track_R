using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IGiroComercialRepository : IRepository<GiroComercial>
    {
        IEnumerable<GiroComercial> ConsultarTodos();
        GiroComercial Consultar(int idGiroComercial);
        GiroComercial ConsultarPorClave(string clave);
        GiroComercial ConsultarPorNombre(string nombre);
        GiroComercial ConsultarDependencias(int idGiroComercial);
    }
}
