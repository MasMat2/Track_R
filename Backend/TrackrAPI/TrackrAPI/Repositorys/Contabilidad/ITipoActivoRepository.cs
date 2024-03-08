using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public interface ITipoActivoRepository : IRepository<TipoActivo>
    {
        IEnumerable<TipoActivoDto> ConsultarParaSelector(int idCompania);
        TipoActivo ConsultarPorClave(string clave);
    }
}
