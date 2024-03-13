using CanalDistAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IBitacoraMovimientoUsuarioRepository : IRepository<BitacoraMovimientoUsuario>
    {
        IEnumerable<BitacoraMovimientoUsuario> consultarFiltroParaPdf(BitacoraMovimientoUsuarioFiltroDto filtro);
    }
}