using TrackrAPI.Dtos;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface ITipoUsuarioRepository : IRepository<TipoUsuario>
    {
        public TipoUsuarioDto ConsultarDto(string clave);
        public TipoUsuario Consultar(int idTipoUsuario);
        public TipoUsuarioDto ConsultarDto(int idTipoUsuario);
        public IEnumerable<TipoUsuarioDto> ConsultarTiposUsuarioSelector();
    }
}
