using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using System.Collections.Generic;
using TrackrAPI.Dtos.Seguridad;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IRolAccesoRepository : IRepository<RolAcceso>
    {
        RolAcceso Consultar(int idRolAcceso);
        IEnumerable<RolAcceso> ConsultarTodosParaSelector();
        RolAcceso ConsultarPorClave(string clave);
    }
}
