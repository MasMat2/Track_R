using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Distribucion
{
    public interface ITipoEmpleadoRepository : IRepository<TipoEmpleado> {
        TipoEmpleado Consultar(int idTipoEmpleado);

        List<TipoEmpleado> ConsultarPorCompania(int idCompania);

        TipoEmpleado Consultar(string clave);

    }
}
