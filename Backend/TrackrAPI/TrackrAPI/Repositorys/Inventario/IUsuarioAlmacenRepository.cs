using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Inventario
{
    public interface IUsuarioAlmacenRepository : IRepository<UsuarioAlmacen>
    {
        public IEnumerable<UsuarioAlmacenDto> ConsultarPorAlmacenDto(int idAlmacen);
        public IEnumerable<UsuarioAlmacen> ConsultarPorAlmacen(int idAlmacen);
        public UsuarioAlmacen Consultar(int idUsuarioAlmacen);
    }
}
