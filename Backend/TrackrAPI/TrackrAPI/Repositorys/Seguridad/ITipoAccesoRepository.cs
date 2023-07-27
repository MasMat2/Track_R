using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface ITipoAccesoRepository : IRepository<TipoAcceso>
    {
        IEnumerable<TipoAcceso> ConsultarGeneral();
    }
}
